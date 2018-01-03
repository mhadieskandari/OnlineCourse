using System;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.WorkFlows.UpdateAccount;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core.WorkFlows
{
    public class UserUpdate:ServiceBase, IDisposable
    {
        private readonly Execution _execution;
        private readonly Validation _validate;
        private readonly Finally _finally;

        public UserUpdate(IServiceProvider serviceProvider, MessageService authMessageSender, HistoryService historyService)
            : base(serviceProvider)
        {
            _validate = new Validation(serviceProvider,historyService);
            _execution=new Execution(serviceProvider,historyService);
            _finally=new Finally(serviceProvider,historyService,authMessageSender);
        }

        public byte Update(User user)
        {
            var isvalid = _validate.Validate(user);
            if (isvalid == (byte) UpdateUserMessage.Success)
            {
                var reg = _execution.Execute(user);
                if (reg == (byte)UpdateUserMessage.Success)
                {
                    var fnl = _finally.Finallize(user);
                    return fnl;
                }
                else
                {
                    return reg;
                }
            }
            else
            {
                return isvalid;
            }
        }


        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
