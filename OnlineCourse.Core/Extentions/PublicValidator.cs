using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Core.Extentions
{
    public class PublicValidator
    {
        public static bool IpCheck(string ip)
        {
            return true;
        }

        public static bool PasswordCheck(string password)
        {
            return true;
        }

        public static bool ConfirmPasswordCheck(string password, string confirmPassword)
        {
            if (password.Equals(confirmPassword)) return true;
            return false;
        }

        public static bool EmailCheck(string email)
        {
            return true;
        }

        public static bool MobileCheck(string moblie)
        {
            return true;
        }
    }
}
