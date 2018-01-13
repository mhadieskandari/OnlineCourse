using System;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Entity;

namespace OnlineCourse.Core.WorkFlows.UpdateAccount
{
    public class Validation : ServiceBase
    {
        private readonly HistoryService _historyService;
        public Validation(IServiceProvider serviceProvider,HistoryService historyService) : base(serviceProvider)
        {
            _historyService = historyService;
        }

        public byte Validate(User user)
        {
            var res = (UpdateUserMessage)ValidateIp(user);
            if (res != UpdateUserMessage.Success)
            {
                return ValidateIp(user);
            }

            if (!string.IsNullOrEmpty(user.Email))
            {
                res = (UpdateUserMessage)ValidateByEmail(user);

                if (res != UpdateUserMessage.Success)
                {
                    return (byte)res;
                }
            }
            //else if (string.IsNullOrEmpty(user.Mobile))
            //{
            //    res = ValidateByEmail(user);
            //    if (res != (byte)RegisterUserMessage.Success)
            //    {
            //        return res;
            //    }
            //}
            else
            {
                return (byte)UpdateUserMessage.MobileAndEmailIsNull;
            }

            if (!PublicValidator.PasswordCheck(user.Password))
            {
                return (byte)UpdateUserMessage.PasswordIsNotValid;
            }



            return (byte)UpdateUserMessage.Success;
        }

        private byte ValidateIp(User user)
        {
            if (PublicValidator.IpCheck(user.LastLoginIp))
            {
                return (byte)UpdateUserMessage.Success;
            }
            else
            {
                return (byte)UpdateUserMessage.IpIsNotValid;
            }
        }

        public byte ValidateByEmail(User user)
        {
            using (var uw = CreateUnitOfWork())
            {
                //if (!Regex.IsMatch(user.Email, RegexConstatnts.EmailRegex))
                //{
                //    return (byte)RegisterUserMessage.EmailPatternNotValid;
                //}
                //else
                try
                {
                    var dbUser = uw.Users.Get(user.Id);
                    if (dbUser==null )
                    {
                        return (byte)UpdateUserMessage.EmailIsNotExist;
                    }
                    else
                    {
                        return (byte)UpdateUserMessage.Success;
                    }
                }
                catch (Exception e)
                {
                   _historyService.LogError(e,HistoryErrorType.Core);
                    throw;
                }
               
            }
        }

        public byte ValidateByMobile(User user)
        {
            using (var uw = CreateUnitOfWork())
            {

                //if (!Regex.IsMatch(user.Mobile, RegexConstatnts.MobileRegex))
                //{
                //    return (byte)RegisterUserMessage.MobilePatternNotValid;
                //}
                //else 
                try
                {
                    if (uw.Users.IsExistEmail(user.Email))
                    {
                        return (byte)UpdateUserMessage.MobileIsNotExist;
                    }
                    else
                    {
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
}
