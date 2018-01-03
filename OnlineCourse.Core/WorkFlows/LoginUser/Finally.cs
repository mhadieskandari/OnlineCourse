using System;
using System.Linq;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core.WorkFlows.LoginUser
{
    public class Finally:ServiceBase
    {

        private readonly HistoryService _historyService;
        private readonly MessageService _messageService;
        public Finally(IServiceProvider serviceProvider,HistoryService historyService,MessageService messageService) : base(serviceProvider)
        {
            _historyService = historyService;
            _messageService = messageService;
        }

        public byte Finallize(LoginDto user)
        {
            try
            {
                using (var uw=CreateUnitOfWork())
                {
                    var usr = uw.Users.GetByEmail(user.Email).FirstOrDefault();
                    if (usr == null)
                    {
                        return (byte) LoginUserMessage.EmailIsNotExist;
                    }
                    return (byte)LoginUserMessage.Success;
                }
            }
            catch (Exception e)
            {
                _historyService.LogError(e,HistoryErrorType.Core);
                throw;
            }
        }


    }
}
