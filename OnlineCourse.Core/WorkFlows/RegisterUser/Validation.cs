using System;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;
using System.Text.RegularExpressions;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Entity;

namespace OnlineCourse.Core.WorkFlows.RegisterUser
{
    public class Validation : ServiceBase
    {
        private readonly HistoryService _historyService;
        public Validation(IServiceProvider serviceProvider, HistoryService historyService) : base(serviceProvider)
        {
            _historyService = historyService;
        }

        public byte Validate(User user)
        {
            var res = ValidateIp(user);
            if (res != (byte)RegisterUserMessage.Success)
            {
                return res;
            }

            if (!string.IsNullOrEmpty(user.Email))
            {
                res = ValidateByEmail(user);
                if (res != (byte)RegisterUserMessage.Success)
                {
                    return res;
                }
            }
            else
            {
                return (byte)RegisterUserMessage.MobileAndEmailIsNull;
            }

            if (!string.IsNullOrEmpty(user.Mobile))
            {
                res = ValidateByMobile(user);
                if (res != (byte)RegisterUserMessage.Success)
                {
                    return res;
                }
            }
            else
            {
                return (byte)RegisterUserMessage.MobileAndEmailIsNull;
            }


            if (!PublicValidator.PasswordCheck(user.Password))
            {
                return (byte)RegisterUserMessage.PasswordIsNotValid;
            }

            return (byte)RegisterUserMessage.Success;
        }

        private byte ValidateIp(User user)
        {
            if (PublicValidator.IpCheck(user.LastLoginIp))
            {
                return (byte)RegisterUserMessage.Success;
            }
            else
            {
                return (byte)RegisterUserMessage.IpIsNotValid;
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
                    if (uw.Users.IsExistEmail(user.Email))
                    {
                        return (byte)RegisterUserMessage.EmailIsExist;
                    }
                    else
                    {
                        return (byte)RegisterUserMessage.Success;
                    }
                }
                catch (Exception e)
                {
                    _historyService.LogError(e, HistoryErrorType.Core);
                    throw;
                }

            }
        }

        public byte ValidateByMobile(User user)
        {
            using (var uw = CreateUnitOfWork())
            {

                if (!Regex.IsMatch(user.Mobile, RegexConstatnts.MobileRegex))
                {
                    return (byte)RegisterUserMessage.MobilePatternNotValid;
                }
                else
                    try
                    {
                        if (uw.Users.IsExistMobile(user.Mobile))
                        {
                            return (byte)RegisterUserMessage.MobileIsExist;
                        }
                        else
                        {
                            return (byte)RegisterUserMessage.Success;
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
