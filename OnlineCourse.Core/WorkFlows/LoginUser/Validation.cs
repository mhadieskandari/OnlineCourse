using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Entity;

namespace OnlineCourse.Core.WorkFlows.LoginUser
{
    public class Validation : ServiceBase
    {
        private readonly HistoryService _historyService;
        public Validation(IServiceProvider serviceProvider, HistoryService historyService)
            : base(serviceProvider)
        {
            _historyService = historyService;
        }

        public byte Validate(LoginDto login)
        {
            try
            {
                using (var uw = CreateUnitOfWork())
                {
                    if (!uw.Users.IsExistEmail(login.Email))
                        return (byte)LoginUserMessage.EmailIsNotExist;

                    var user = uw.Users.GetByEmail(login.Email).FirstOrDefault(m => EncryptDecrypt.Decrypt(m.Password).Equals(login.PassWord));
                   
                    if (user == null)
                        return (byte)LoginUserMessage.PasswordInCorrect;

                    if (user.ValidMobile != (byte) ValidationState.Valid)
                        return (byte) LoginUserMessage.AccountNotVerified;

                    if (user.State != UserState.Approved && (user.AccessLevel==UserAccessLevel.Administrator  || user.AccessLevel ==UserAccessLevel.Teacher))
                        return (byte)LoginUserMessage.AccountDisabled;

                    if (user.State != UserState.Verified && user.State != UserState.Approved)
                        return (byte)LoginUserMessage.AccountNotVerified;

                    if (!PublicValidator.IpCheck(login.Ip))
                        return (byte)LoginUserMessage.IpNotValid;
                }

                return (byte)LoginUserMessage.Success;
            }
            catch (Exception e)
            {
                _historyService.LogError(e,HistoryErrorType.Core);
                throw;
            }
            
        }
    }
}
