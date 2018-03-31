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

        private readonly bool _isAdmin;


        public Execution(IServiceProvider serviceProvider, HistoryService historyService, bool isAdmin) : base(serviceProvider)
        {
            _historyService = historyService;
            _isAdmin = isAdmin;
        }

        public byte Execute(User user)
        {
            try
            {
                using (var uw = CreateUnitOfWork())
                {
                    var dbuser = uw.Users.Get(user.Id);

                    var securitySpan = Guid.NewGuid().ToString();
                    if (_isAdmin)
                    {
                        if (dbuser.AccessLevel != user.AccessLevel)
                        {
                            dbuser.AccessLevel = user.AccessLevel;
                            dbuser.SecuritySpan = securitySpan;
                        }

                        dbuser.Description = user.Description;

                        if (user.ExpireDate != null && dbuser.ExpireDate != user.ExpireDate)
                        {
                            dbuser.ExpireDate = user.ExpireDate;
                            dbuser.SecuritySpan = securitySpan;
                        }

                        if (dbuser.State != user.State)
                        {
                            dbuser.State = user.State;
                            dbuser.SecuritySpan = securitySpan;
                        }
                        if (!string.IsNullOrEmpty(user.Mobile) && !dbuser.Mobile.Equals(user.Mobile))
                        {
                            dbuser.Mobile = user.Mobile;
                            dbuser.ValidMobile = (byte)ValidationState.Invalid;
                            if (dbuser.AccessLevel == UserAccessLevel.Stusent ||
                                dbuser.AccessLevel == UserAccessLevel.Teacher)
                            {
                                dbuser.ActivationCode = null;
                            }
                        }
                    }

                    dbuser.Addrress = user.Addrress;
                    dbuser.City = user.City;
                    dbuser.Phone = user.Phone;
                    dbuser.FullName = user.FullName;
                   
                    if (!string.IsNullOrEmpty(user.Email) && !dbuser.Email.Equals(user.Email))
                    {
                        dbuser.Email = user.Email;
                        dbuser.ValidEmail = (byte)ValidationState.Invalid;
                        if (dbuser.AccessLevel == UserAccessLevel.Stusent ||
                            dbuser.AccessLevel == UserAccessLevel.Teacher)
                        {
                            dbuser.ActivationCode = null;
                        }
                    }
                    uw.Users.Update(dbuser);
                    uw.Complete();
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
