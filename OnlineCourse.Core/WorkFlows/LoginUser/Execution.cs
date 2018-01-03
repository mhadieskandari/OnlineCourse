using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core.WorkFlows.LoginUser
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

        public byte Execute(LoginDto login)
        {
            using (var uw = CreateUnitOfWork())
            {
                var userList = uw.Users.GetByEmail(login.Email);
                try
                {
                    var dbUser = userList.FirstOrDefault();
                    if (dbUser != null)
                    {
                        dbUser.LastLoginDate = DateTime.Now;
                        dbUser.LastLoginIp = login.Ip;
                        uw.Complete();
                    }
                    return (byte)LoginUserMessage.Success;
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
