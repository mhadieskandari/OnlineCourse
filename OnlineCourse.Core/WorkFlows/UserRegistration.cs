using System;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.WorkFlows.RegisterUser;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core.WorkFlows
{
    public class UserRegistration:ServiceBase
    {
        private readonly Execution _execution;
        private readonly Validation _validate;
        private readonly Finally _finally;

        public UserRegistration(IServiceProvider serviceProvider, MessageService messageService, HistoryService historyService)
            : base(serviceProvider)
        {
            _validate = new Validation(serviceProvider,historyService);
            _execution=new Execution(serviceProvider,historyService);
            _finally=new Finally(serviceProvider,historyService, messageService);
        }

        public byte Register(User user)
        {
            var isvalid = _validate.Validate(user);
            if (isvalid == (byte) RegisterUserMessage.Success)
            {
                var reg = _execution.Execute(user);
                if (reg == (byte) RegisterUserMessage.Success)
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










    }
}
