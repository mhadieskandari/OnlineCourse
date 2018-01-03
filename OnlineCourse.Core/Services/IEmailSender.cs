using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Core.Services
{
    public interface IEmailSender
    {
        long SendEmailAsync(string email, string subject, string message);
    }
}
