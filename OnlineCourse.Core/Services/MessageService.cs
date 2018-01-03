using Kavenegar;
using Kavenegar.Core.Exceptions;
using System;
using System.Threading.Tasks;

namespace OnlineCourse.Core.Services
{    
    public class MessageService : IEmailSender, ISmsSender
    {
        private string VerificationTemplate = "NegarFoodVer";
        private readonly KavenegarApi api = new KavenegarApi("33697A3278307456414E336A435A787A5471636147773D3D");
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        public long SendSmsAsync(string number, string message)
        {
            try
            {
                var result = api.Send("10009192848236", number, message).Result;

                return result.Messageid;
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
                throw;
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
                throw;
            }
        }

        public long Verification(string number, string message)
        {
            try
            {
                var result = api.VerifyLookup(number, message, VerificationTemplate).Result;

                return result.Messageid;
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
                throw;
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
                throw;
            }
        }

        long IEmailSender.SendEmailAsync(string email, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
