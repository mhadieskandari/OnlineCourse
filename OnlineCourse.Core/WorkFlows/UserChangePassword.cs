using System;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.WorkFlows.ChangePassword;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core.WorkFlows
{
    public class UserChangePassword : ServiceBase, IDisposable
    {
        private readonly Execution _execution;
        private readonly Validation _validate;
        private readonly Finally _finally;

        public UserChangePassword(IServiceProvider serviceProvider, MessageService authMessageSender, HistoryService historyService)
            : base(serviceProvider)
        {
            _validate = new Validation(serviceProvider,historyService);
            _execution=new Execution(serviceProvider,historyService);
            _finally=new Finally(serviceProvider,historyService,authMessageSender);
        }

        public byte CahngePassword(ChangePasswordDto modelDto)
        {
            var isvalid = _validate.Validate(modelDto);
            if (isvalid == (byte) UpdateUserMessage.Success)
            {
                var reg = _execution.Execute(modelDto);
                if (reg == (byte)UpdateUserMessage.Success)
                {
                    var fnl = _finally.Finallize(modelDto);
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
