using System;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.WorkFlows.LoginUser;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core.WorkFlows
{
    public class UserLogin:ServiceBase
    {
        private readonly Execution _execution;
        private readonly Validation _validate;
        private readonly Finally _finally;

        public UserLogin(IServiceProvider serviceProvider, MessageService messageService, HistoryService historyService)
            : base(serviceProvider)
        {
            _validate = new Validation(serviceProvider,historyService);
            _execution=new Execution(serviceProvider,historyService);
            _finally=new Finally(serviceProvider,historyService, messageService);
        }

        public byte Login(LoginDto login)
        {
            var isvalid = _validate.Validate(login);
            if (isvalid == (byte) LoginUserMessage.Success)
            {
                var logExec = _execution.Execute(login);
                if (logExec == (byte)LoginUserMessage.Success)
                {
                    var fnl = _finally.Finallize(login);
                    return fnl;
                }
                else
                {
                    return logExec;
                }
            }
            else
            {
                return isvalid;
            }
        }










    }
}
