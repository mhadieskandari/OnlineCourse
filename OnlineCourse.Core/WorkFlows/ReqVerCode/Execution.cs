using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;
using OnlineCourse.Core.Extentions;

namespace OnlineCourse.Core.WorkFlows.ReqVerCode
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

        public byte Execute(ReqVerifyCodeDto reqVerifyCodeDto)
        {
            using (var uw = CreateUnitOfWork())
            {
                var userList = uw.Users.GetByEmail(reqVerifyCodeDto.Email);
                try
                {
                    var dbUser = userList.FirstOrDefault();
                    if (dbUser != null)
                    {
                        dbUser.LastLoginIp = reqVerifyCodeDto.Ip;
                        dbUser.LastRequestActivationCode = DateTime.Now;
                        dbUser.ActivationCode = RandomizeHelper.GetNumber(6).ToString();
                        

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
