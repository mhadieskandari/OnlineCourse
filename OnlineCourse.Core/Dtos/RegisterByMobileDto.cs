using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Core.Dtos
{
    public class RegisterByMobileDto
    {
        public string Mobile { set; get; }
        public string PassWord { set; get; }
        public string ConfirmPassword { set; get; }
    }
}
