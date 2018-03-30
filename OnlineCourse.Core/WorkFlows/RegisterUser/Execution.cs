using System;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity.Models;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Entity;

namespace OnlineCourse.Core.WorkFlows.RegisterUser { 

    public class Execution:ServiceBase
    {
        private readonly HistoryService _historyService;

        public Execution(IServiceProvider serviceProvider, HistoryService historyService) : base(serviceProvider)
        {
            _historyService = historyService;
        }

        public byte Execute(User user)
        {
            try
            {
                using (var uw=CreateUnitOfWork())
                {
                    user.Password = user.Password != null?EncryptDecrypt.Encrypt(user.Password) : EncryptDecrypt.Encrypt(RandomizeHelper.GetString(8));
                    //user.AccessLevel = (byte)UserAccessLevel.Customer;
                    user.RegisterDate=DateTime.Now;
                    user.ExpireDate=DateTime.Now.AddDays(30);
                    user.State = UserState.Pending;
                    user.SecuritySpan=Guid.NewGuid().ToString();
                    //user.City = "qaz";
                    uw.Users.Add(user);
                    uw.Complete();
                    return (byte)RegisterUserMessage.Success;
                }
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }

        }

    }
}
