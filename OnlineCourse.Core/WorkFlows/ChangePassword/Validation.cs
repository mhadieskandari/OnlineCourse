using System;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;
using System.Linq;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Entity;

namespace OnlineCourse.Core.WorkFlows.ChangePassword
{
    public class Validation : ServiceBase
    {
        private readonly HistoryService _historyService;
        public Validation(IServiceProvider serviceProvider,HistoryService historyService) : base(serviceProvider)
        {
            _historyService = historyService;
        }

        public byte Validate(ChangePasswordDto user)
        {

            var res = (ChangePasswordUserMessage)ValidateIp(user);
            if (res != ChangePasswordUserMessage.Success)
            {
                return ValidateIp(user);
            }

            if (!string.IsNullOrEmpty(user.UserName))
            {
                res = (ChangePasswordUserMessage)ValidateByEmail(user);

                if (res != ChangePasswordUserMessage.Success)
                {
                    return (byte)res;
                }
            }
            else
            {
                return (byte)ChangePasswordUserMessage.MobileAndEmailIsNull;
            }


            var PassCheck = ValidateOldPass(user);
            if (PassCheck != (byte)ChangePasswordUserMessage.Success)
                return PassCheck;


            if (!PublicValidator.PasswordCheck(user.Password))
            {
                return (byte)ChangePasswordUserMessage.PasswordIsNotValid;
            }

            if (!PublicValidator.PasswordCheck(user.NewPassword))
            {
                return (byte)ChangePasswordUserMessage.NewPasswordNotValid;
            }


            if (!PublicValidator.ConfirmPasswordCheck(user.NewPassword,user.ConfirmNewPassword))
            {
                return (byte)ChangePasswordUserMessage.ConfirmNewPasswordNotValid;
            }





            return (byte)ChangePasswordUserMessage.Success;
        }

        private byte ValidateIp(ChangePasswordDto user)
        {
            if (PublicValidator.IpCheck(user.Ip))
            {
                return (byte)ChangePasswordUserMessage.Success;
            }
            else
            {
                return (byte)ChangePasswordUserMessage.IpIsNotValid;
            }
        }

        public byte ValidateByEmail(ChangePasswordDto user)
        {
            using (var uw = CreateUnitOfWork())
            {
                try
                {
                    var dbUser = uw.Users.GetByEmail(user.UserName);
                    if (dbUser==null )
                    {
                        return (byte)ChangePasswordUserMessage.EmailIsNotExist;
                    }
                    else
                    {
                        return (byte)ChangePasswordUserMessage.Success;
                    }
                }
                catch (Exception e)
                {
                   _historyService.LogError(e,HistoryErrorType.Core);
                    throw;
                }
               
            }
        }

        public byte ValidateOldPass(ChangePasswordDto user)
        {
            using (var uw = CreateUnitOfWork())
            {
                try
                {
                    var dbUser = uw.Users.GetByEmail(user.UserName).SingleOrDefault();
                    if (dbUser != null && user.Password == EncryptDecrypt.Decrypt(dbUser.Password))
                    {
                        return (byte)ChangePasswordUserMessage.Success;
                    }
                    else
                    {
                        return (byte)ChangePasswordUserMessage.PasswordIsNotValid;
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
}
