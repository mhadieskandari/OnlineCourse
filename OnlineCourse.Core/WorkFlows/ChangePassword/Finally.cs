using System;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core.WorkFlows.ChangePassword
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

        public byte Finallize(ChangePasswordDto user)
        {
            try
            {
                _messageSender.SendEmailAsync(user.UserName, "ChangePassword", "ChangePassword Success");
                return (byte)ChangePasswordUserMessage.Success;
            }
            catch (Exception e)
            {
                _historyService.LogError(e,HistoryErrorType.Core);
                throw;
            }
        }


    }
}
