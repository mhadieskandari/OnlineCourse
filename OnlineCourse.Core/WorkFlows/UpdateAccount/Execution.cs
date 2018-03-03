using System;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Entity;

namespace OnlineCourse.Core.WorkFlows.UpdateAccount
{

    public class Execution : ServiceBase
    {
        private readonly HistoryService _historyService;

        public Execution(IServiceProvider serviceProvider, HistoryService historyService) : base(serviceProvider)
        {
            _historyService = historyService;
        }

        public byte Execute(User user)
        {

            bool withLogin = false;
            try
            {
                using (var uw = CreateUnitOfWork())
                {
                    var dbuser = uw.Users.Get(user.Id);

                    var securitySpan = Guid.NewGuid().ToString();

                    if (user.AccessLevel != null && dbuser.AccessLevel != user.AccessLevel)
                    {
                        dbuser.AccessLevel = user.AccessLevel;
                        dbuser.SecuritySpan = securitySpan;
                    }
                        
                    //else
                    //    dbuser.AccessLevel = (byte)UserAccessLevel.Customer;

                    dbuser.Addrress = user.Addrress;
                    dbuser.City = user.City;
                    dbuser.Description = user.Description;

                    

                    dbuser.Phone = user.Phone;



                    if (user.ExpireDate != null && dbuser.ExpireDate != user.ExpireDate)
                    {
                        dbuser.ExpireDate = user.ExpireDate;
                        dbuser.SecuritySpan = securitySpan;
                    }
                        

                    dbuser.FullName = user.FullName;
                    
                    if (user.State != null && dbuser.State != user.State)
                    {
                        dbuser.State = user.State;
                        dbuser.SecuritySpan = securitySpan;
                    }
                        
                    

                    if (!string.IsNullOrEmpty(user.Password) && !EncryptDecrypt.Decrypt(dbuser.Password).Equals(user.Password))
                    {
                        dbuser.Password = EncryptDecrypt.Encrypt(user.Password);
                        withLogin = true;
                    }
                        

                    dbuser.Position = user.Position;


                    if (!string.IsNullOrEmpty(user.Mobile) && !dbuser.Mobile.Equals(user.Mobile))
                    {
                        dbuser.Mobile = user.Mobile;
                        dbuser.ValidMobile = (byte)ValidationState.Invalid;
                        if (dbuser.AccessLevel == UserAccessLevel.Stusent ||
                            dbuser.AccessLevel == UserAccessLevel.Teacher)
                        {
                            dbuser.ActivationCode = null;
                        }
                        withLogin = true;
                    }
                    if (!string.IsNullOrEmpty(user.Email) && !dbuser.Email.Equals(user.Email))
                    {
                        dbuser.Email = user.Email;
                        //dbuser.ValidEmail = (byte)ValidationState.Invalid;

                        if (dbuser.AccessLevel == UserAccessLevel.Stusent ||
                            dbuser.AccessLevel == UserAccessLevel.Teacher)
                        {
                            dbuser.ActivationCode = null;
                        }
                        //withLogin = true;
                    }


                    uw.Users.Update(dbuser);
                    var res = uw.Complete();

                    if (res > 0 && withLogin)
                        return (byte) UpdateUserMessage.SuccessWithLogin;

                    return (byte)UpdateUserMessage.Success;
                }
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }
    }
}
