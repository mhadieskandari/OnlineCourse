using System;
using System.Linq;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Entity;

namespace OnlineCourse.Core.WorkFlows.RecoveryUser
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
                using (var uw=CreateUnitOfWork())
                {
                    var usr = uw.Users.GetByMobile(user.Mobile);
                    if (usr == null)
                    {
                        return (byte) RecoveryUserMessage.EmailIsNotExist;
                    }
                    //todo recoverPassword Or ResetPassword
                    var tempPwd = EncryptDecrypt.Decrypt(usr.Password);
                    var smsId=_messageSender.Verification(usr.Mobile, tempPwd);
                    return (byte)RegisterUserMessage.Success;
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
