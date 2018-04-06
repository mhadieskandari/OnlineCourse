using System;
using System.Linq;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Entity;

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
                    if (!PublicValidator.IpCheck(reqVerifyCodeDto.Ip))
                        return (byte)VerifyUserMessage.IpNotValid;
                    
                    //todo this is a big bug check this and resolve fix bug
                    if (item.LastRequestActivationCode.HasValue && DateTime.Now.Subtract(item.LastRequestActivationCode.Value).Minutes < 2 && !string.IsNullOrEmpty(item.ActivationCode))
                        return (byte)VerifyUserMessage.ActivationCodeSend;
                    //todo end
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
