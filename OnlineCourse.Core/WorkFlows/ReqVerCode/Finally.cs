using System;
using System.Linq;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;

namespace OnlineCourse.Core.WorkFlows.ReqVerCode
{
    public class Finally : ServiceBase
    {

        private readonly HistoryService _historyService;
        private readonly MessageService _messageSender;
        public Finally(IServiceProvider serviceProvider, HistoryService historyService, MessageService messageSender) : base(serviceProvider)
        {
            _historyService = historyService;
            _messageSender = messageSender;
        }

        public byte Finallize(ReqVerifyCodeDto reqVerifyCodeDto)
        {
            try
            {
                using (var uw = CreateUnitOfWork())
                {
                    var usr = uw.Users.GetByEmail(reqVerifyCodeDto.Email).FirstOrDefault();
                    if (usr == null)
                    {
                        return (byte)VerifyUserMessage.EmailIsNotExist;
                    }
                    var res = _messageSender.Verification(usr.Mobile, usr.ActivationCode);
                    if (res > 0)
                        return (byte)VerifyUserMessage.Success;
                    else
                        return (byte)VerifyUserMessage.Exception;
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
