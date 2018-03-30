using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Core.Dtos
{
    public class ChangePasswordDto
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public string NewPassword { set; get; }
        public string ConfirmNewPassword { set; get; }
        public string Ip { set; get; }

        public bool IsAdmin { set; get; } = false;
    }
}
