using System;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.WorkFlows.VerifyUser;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core.WorkFlows
{
    public class UserVerify : ServiceBase, IDisposable
    {
        private readonly Execution _execution;
        private readonly Validation _validate;
        private readonly Finally _finally;

        public UserVerify(IServiceProvider serviceProvider, MessageService authMessageSender, HistoryService historyService)
            : base(serviceProvider)
        {
            _validate = new Validation(serviceProvider,historyService);
            _execution=new Execution(serviceProvider,historyService);
            _finally=new Finally(serviceProvider,historyService,authMessageSender);
        }

        public byte Update(VerifyDto verifyDto)
        {
            var isvalid = _validate.Validate(verifyDto);
            if (isvalid == (byte) UpdateUserMessage.Success)
            {
                var reg = _execution.Execute(verifyDto);
                if (reg == (byte)UpdateUserMessage.Success)
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
