using System;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;
using OnlineCourse.Entity;

namespace OnlineCourse.Core.WorkFlows.UpdateAccount
{
    public class Finally:ServiceBase
    {

        private readonly HistoryService _historyService;
        private readonly MessageService _messageSender;
        public Finally(IServiceProvider serviceProvider,HistoryService historyService,MessageService messageSender) : base(serviceProvider)
        {
            _historyService = historyService;
            _messageSender = messageSender;
        }

        public byte Finallize(User user)
        {
            try
            {
                _messageSender.SendEmailAsync(user.Email, "UpdateAccount", "UpdateAccount Suuccess");
                return (byte)UpdateUserMessage.Success;
            }
            catch (Exception e)
            {
                _historyService.LogError(e,HistoryErrorType.Core);
                throw;
            }
        }


    }
}
