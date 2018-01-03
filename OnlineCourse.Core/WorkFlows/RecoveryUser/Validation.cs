using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core.WorkFlows.RecoveryUser
{
    public class Validation : ServiceBase
    {
        private readonly HistoryService _historyService;
        public Validation(IServiceProvider serviceProvider,HistoryService historyService)
            : base(serviceProvider)
        {
            _historyService = historyService;
        }

        public byte Validate(User user)
        {
            byte res;
            using (var uw = CreateUnitOfWork())
            {
                //if (!string.IsNullOrEmpty(user.Email))
                //{
                //    if (!uw.Users.IsExistEmail(user.Email))
                //    {
                //        res = (byte)RecoveryUserMessage.EmailIsNotExist;
                //    }
                //    else
                //    {
                //        res = (byte)RecoveryUserMessage.Success;
                //    }
                //}
                //else
                //{
                    if (!uw.Users.IsExistMobile(user.Mobile))
                    {
                        res = (byte)RecoveryUserMessage.MobileIsNotExist;
                    }
                    else
                    {
                        res = (byte)RecoveryUserMessage.Success;
                    }
                //}

                var item = uw.Users.GetByMobile(user.Mobile);
                if (item != null)
                {
                    TimeSpan diff;
                    try
                    {
                        if (item.LastResetPasswordDate != null)
                        {
                            diff = DateTime.Now.Subtract(item.LastResetPasswordDate.Value);
                            if (diff.Minutes < 2)
                            {
                                res = (byte)RecoveryUserMessage.LastResetIsClose;
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        _historyService.LogError(e,HistoryErrorType.Core);
                        throw;
                    }
                }
            }
            return res;
        }
    }
}
