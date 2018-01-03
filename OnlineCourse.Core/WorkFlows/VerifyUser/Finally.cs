using System;
using System.Linq;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core.WorkFlows.VerifyUser
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

        public byte Finallize(VerifyDto verifyDto)
        {
            try
            {
                using (var uw=CreateUnitOfWork())
                {
                    var usr = uw.Users.GetByEmail(verifyDto.Email).FirstOrDefault();
                    if (usr == null)
                    {
                        return (byte) VerifyUserMessage.EmailIsNotExist;
                    }
                    _messageSender.SendEmailAsync(usr.Email, "Verification", "Your Account Verified.");
                    return (byte)VerifyUserMessage.Success;
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
