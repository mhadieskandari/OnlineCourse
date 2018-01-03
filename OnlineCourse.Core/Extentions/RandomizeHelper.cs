using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Core.Extentions
{
    public class RandomizeHelper
    {
        public static long GetNumber(int max)
        {
            try
            {
                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                var sOtp = string.Empty;
                var rand = new Random();
                for (var i = 0; i < max; i++)

                {
                    var sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sOtp += sTempChars;
                }
                return long.Parse(sOtp);
            }
            catch (Exception ex)
            {
                //Chistory.RaiseError(ex, HistoryAction.MiddleLayout);
                throw;
            }
        }

        public static String GetString(int length)
        {
            try
            {
                Random rnd = new Random();
                string result = "";
                for (var i = 0; i < length; i++)
                {
                    var num = rnd.Next(97, 122);
                    result += (char)num;
                }
                return result;
            }
            catch (Exception ex)
            {
                //Chistory.RaiseError(ex, HistoryAction.MiddleLayout);
                throw;
            }
        }
    }
}
