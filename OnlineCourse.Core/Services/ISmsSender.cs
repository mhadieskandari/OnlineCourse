using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Core.Services
{
    public interface ISmsSender
    {
        long SendSmsAsync(string number, string message);
        long Verification(string number, string message);
    }
}
