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
            else
            {
                return (byte)UpdateUserMessage.MobileAndEmailIsNull;
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
