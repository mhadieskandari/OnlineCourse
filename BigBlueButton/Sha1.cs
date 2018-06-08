using System;
using System.Text;

namespace BigBlueButton
{
    public class Sha1
    {
        public static string GetSha1(string strValue)
        {
            var bytes = Encoding.UTF8.GetBytes(strValue);

            // encrypt bytes
            var sha1 = System.Security.Cryptography.SHA1.Create();
            
            var hashBytes = sha1.ComputeHash(bytes);

            // Convert the encrypted bytes back to a string (base 16)
            var hashString = "";

            foreach (byte t in hashBytes)
            {
                hashString += Convert.ToString(t, 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }
    }
}
