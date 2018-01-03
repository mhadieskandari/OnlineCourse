using System;
using System.Linq;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.Extentions;

namespace OnlineCourse.Core.WorkFlows.ChangePassword
{

    public class Execution : ServiceBase
    {
        private readonly HistoryService _historyService;

        public Execution(IServiceProvider serviceProvider, HistoryService historyService) : base(serviceProvider)
        {
            _historyService = historyService;
        }

        public byte Execute(ChangePasswordDto user)
        {
            try
            {
                using (var uw = CreateUnitOfWork())
                {
                    var dbuser = uw.Users.GetByEmail(user.UserName).FirstOrDefault();
                    if (dbuser != null)
                    {
                        dbuser.SecuritySpan = Guid.NewGuid().ToString();
                        dbuser.Password = EncryptDecrypt.Encrypt(user.NewPassword);
                        uw.Users.Update(dbuser);
                        var res = uw.Complete();
                        if(res>0)return (byte)ChangePasswordUserMessage.SuccessWithLogin;
                    }
                    return (byte)ChangePasswordUserMessage.Success;
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
