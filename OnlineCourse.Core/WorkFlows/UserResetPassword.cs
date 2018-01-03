using System;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.WorkFlows.RecoveryUser;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core.WorkFlows
{
    public class UserResetPassword:ServiceBase
    {
        private readonly Execution _execution;
        private readonly Validation _validate;
        private readonly Finally _finally;


        public UserResetPassword(IServiceProvider serviceProvider, MessageService authMessageSender, HistoryService historyService)
            : base(serviceProvider)
        {
            _validate = new Validation(serviceProvider,historyService);
            _execution = new Execution(serviceProvider,historyService);
            _finally=new Finally(serviceProvider,historyService,authMessageSender);
        }

        public byte Recovery(User user)
        {
            var isVal = _validate.Validate(user);

            if (isVal == (byte) RecoveryUserMessage.Success)
            {
                isVal=_execution.Execute(user);
                if(isVal == (byte)RecoveryUserMessage.Success)
                {
                    var fnl = _finally.Finallize(user);
                    return fnl;
                }
                return isVal;                
            }
            else
            {
                return isVal;
            }
        }

        public byte Reset(User user)
        {
            var isVal = _validate.Validate(user);

            if (isVal == (byte)RecoveryUserMessage.Success)
            {
                var exec = _execution.Execute(user);
                if (exec == (byte)RecoveryUserMessage.Success)
                {
                    var fnl = _finally.Finallize(user);
                    return fnl;
                }
                else
                {
                    return exec;
                }
            }
            else
            {
                return isVal;
            }
        }

    }
}
