using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineCourse.Core;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.WorkFlows.RecoveryUser;
using OnlineCourse.Entity.Models;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Entity;

namespace OnlineCourse.Core.WorkFlows.RecoveryUser
{

    //private   AuthMessageSender
    public class Execution : ServiceBase
    {
        private readonly HistoryService _historyService;

        public Execution(IServiceProvider serviceProvider, HistoryService historyService)
            : base(serviceProvider)
        {
            _historyService = historyService;
        }

        public byte Execute(User user)
        {
            using (var uw = CreateUnitOfWork())
            {
                var dbUser = uw.Users.GetByMobile(user.Mobile);
                if (dbUser!=null)
                {
                    try
                    {
                        string tempPwd = RandomizeHelper.GetString(8);
                        dbUser.Password = EncryptDecrypt.Encrypt(tempPwd);
                        dbUser.SecuritySpan = Guid.NewGuid().ToString();
                        dbUser.LastResetPasswordDate = DateTime.Now;
                        uw.Users.Update(dbUser);
                        var res = uw.Complete();
                        if (res > 0)
                            return (byte) RecoveryUserMessage.Success;
                        else
                            return (byte) RecoveryUserMessage.Exception;
                    }
                    catch (Exception e)
                    {
                        _historyService.LogError(e, HistoryErrorType.Core);
                        throw;
                    }
                }
                else
                {
                    return (byte)RecoveryUserMessage.DuplicateUser;
                }
            }
        }
    }
}
