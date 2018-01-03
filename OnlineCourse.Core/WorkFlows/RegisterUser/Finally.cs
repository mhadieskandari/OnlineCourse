using System;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core.WorkFlows.RegisterUser
{
    public class Finally:ServiceBase
    {

        private readonly HistoryService _historyService;
        private readonly MessageService _messageSender;
        public Finally(IServiceProvider serviceProvider,HistoryService historyService, MessageService messageSender) : base(serviceProvider)
        {
            _historyService = historyService;
            _messageSender = messageSender;
        }


        public byte Finallize(User user)
        {
            try
            {
                _messageSender.SendEmailAsync(user.Email, "Register", "Register Success . active your Account");
                return (byte)RegisterUserMessage.Success;
            }
            catch (Exception e)
            {
                _historyService.LogError(e,HistoryErrorType.Core);
                throw;
            }
        }


    }
}
