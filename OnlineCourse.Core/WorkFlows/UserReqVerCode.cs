using System;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.WorkFlows.ReqVerCode;

namespace OnlineCourse.Core.WorkFlows
{
    public class UserReqVerCode : ServiceBase, IDisposable
    {
        private readonly Execution _execution;
        private readonly Validation _validate;
        private readonly Finally _finally;

        public UserReqVerCode(IServiceProvider serviceProvider, MessageService messageService, HistoryService historyService)
            : base(serviceProvider)
        {
            _validate = new Validation(serviceProvider,historyService);
            _execution=new Execution(serviceProvider,historyService);
            _finally=new Finally(serviceProvider,historyService, messageService);
        }

        public byte RequestCode(ReqVerifyCodeDto verifyDto)
        {
            var isvalid = _validate.Validate(verifyDto);
            if (isvalid == (byte)VerifyUserMessage.Success)
            {
                var reg = _execution.Execute(verifyDto);
                if (reg == (byte)VerifyUserMessage.Success)
                {
                    var fnl = _finally.Finallize(verifyDto);
                    return fnl;
                }
                return reg;
            }
            return isvalid;
        }


        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
