using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;

using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core.WorkFlows.VerifyUser
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

        public byte Execute(VerifyDto verifyDto)
        {
            using (var uw = CreateUnitOfWork())
            {
                var dbUser = uw.Users.GetByEmail(verifyDto.Email).FirstOrDefault();
                try
                {
                    if (dbUser != null)
                    {
                        dbUser.LastLoginIp = verifyDto.Ip;
                        dbUser.State = (byte)UserState.Verified;
                        dbUser.ValidMobile = (byte)ValidationState.Valid;

                        var count=uw.Complete();
                        if(count>0) return (byte)
                                VerifyUserMessage.Success;
                    }
                    return (byte)VerifyUserMessage.Exception;
                }
                catch (Exception e)
                {
                    _historyService.LogError(e, HistoryErrorType.Core);
                    throw;
                }

            }
        }
    }
}
