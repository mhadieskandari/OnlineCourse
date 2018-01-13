using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Entity;

namespace OnlineCourse.Core.WorkFlows.VerifyUser
{
    public class Validation : ServiceBase
    {
        private readonly HistoryService _historyService;
        public Validation(IServiceProvider serviceProvider, HistoryService historyService)
            : base(serviceProvider)
        {
            _historyService = historyService;
        }

        public byte Validate(VerifyDto verifyDto)
        {
            try
            {
                using (var uw = CreateUnitOfWork())
                {
                    if (!uw.Users.IsExistEmail(verifyDto.Email))
                        return (byte)VerifyUserMessage.EmailIsNotExist;

                    var item = uw.Users.GetByEmail(verifyDto.Email).FirstOrDefault();


                    if (item == null)
                    {
                        return (byte) VerifyUserMessage.ActivationCodeInCorrect;
                    }

                    if (item.ValidEmail == (byte)ValidationState.Valid)
                        return (byte)VerifyUserMessage.Success;


                    //if (item.State != (byte)UserState.Approved)
                    //    return (byte)VerifyUserMessage.AccountDisabled;

                    if (!PublicValidator.IpCheck(verifyDto.Ip))
                        return (byte)VerifyUserMessage.IpNotValid;

                    if (item.LastRequestActivationCode.HasValue && DateTime.Now.Subtract(item.LastRequestActivationCode.Value).Minutes > 2 ) 
                        return (byte)VerifyUserMessage.ActivationCodeExpired;

                    if (!item.ActivationCode.Equals(verifyDto.VerificationCode))
                        return (byte) VerifyUserMessage.ActivationCodeInCorrect;
                }

                return (byte)VerifyUserMessage.Success;
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }

        }
    }
}
