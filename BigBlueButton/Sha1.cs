using System;
using System.Security.Cryptography;
using System.Text;

namespace BigBlueButton
{
    public class Sha1
    {
        public static string GetSha1(string strValue)
        {
            
            var bytes = Encoding.UTF8.GetBytes(strValue);

            // encrypt bytes
            //var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1 =SHA1Managed.Create();
            var hashBytes = sha1.ComputeHash(bytes);

            // Convert the encrypted bytes back to a string (base 16)
            var hashString = "";

            foreach (byte t in hashBytes)
            {
                hashString += Convert.ToString(t, 16).PadLeft(2, '0');
            }

            //return hashString.PadLeft(32, '0');
            return hashString.PadLeft(32, '0');
        }

        static string sha256(string randomString)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }

        public static string SHA1HashStringForUTF8String(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            return HexStringFromBytes(hashBytes);
        }

        /// <summary>
        /// Convert an array of bytes to a string of hex digits
        /// </summary>
        /// <param name="bytes">array of bytes</param>
        /// <returns>String of hex digits</returns>
        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
