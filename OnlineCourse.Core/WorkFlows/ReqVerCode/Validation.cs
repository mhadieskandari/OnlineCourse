using System;
using System.Linq;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.Extentions;

namespace OnlineCourse.Core.WorkFlows.ReqVerCode
{
    public class Validation : ServiceBase
    {
        private readonly HistoryService _historyService;
        public Validation(IServiceProvider serviceProvider, HistoryService historyService)
            : base(serviceProvider)
        {
            _historyService = historyService;
        }

        public byte Validate(ReqVerifyCodeDto reqVerifyCodeDto)
        {
            try
            {
                using (var uw = CreateUnitOfWork())
                {
                    if (!uw.Users.IsExistEmail(reqVerifyCodeDto.Email))
                        return (byte)VerifyUserMessage.EmailIsNotExist;

                    var item = uw.Users.GetByEmail(reqVerifyCodeDto.Email).FirstOrDefault();

                    if (item == null)
                    {
                        return (byte) VerifyUserMessage.ActivationCodeInCorrect;
                    }

                    //if (item.ValidEmail == (byte) ValidationState.Valid)
                    //    return (byte) VerifyUserMessage.Success;

                    if (!PublicValidator.IpCheck(reqVerifyCodeDto.Ip))
                        return (byte)VerifyUserMessage.IpNotValid;

                    //if (item.LastRequestActivationCode.HasValue && item.LastRequestActivationCode.Value.Subtract(DateTime.Now).Minutes >20 )
                    //    return (byte)VerifyUserMessage.ActivationCodeExpired;

                    if (item.LastRequestActivationCode.HasValue && DateTime.Now.Subtract(item.LastRequestActivationCode.Value).Minutes < 2 && !string.IsNullOrEmpty(item.ActivationCode))
                        return (byte)VerifyUserMessage.ActivationCodeSend;
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
