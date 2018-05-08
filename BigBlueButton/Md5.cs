using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BigBlueButton
{
    public class Md5
    {
        
        public string encryptString(string strToEncrypt)
        {
            var ue = new UTF8Encoding();
            var bytes = ue.GetBytes(strToEncrypt);

            // encrypt bytes
            var md5 = new MD5CryptoServiceProvider();
            var hashBytes = md5.ComputeHash(bytes);

            // Convert the encrypted bytes back to a string (base 16)
            var hashString = "";

            foreach (byte t in hashBytes)
            {
                hashString += Convert.ToString(t, 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }

        public string encryptString(string strToEncryp, int algorithm)
        {
            if (algorithm == 1)
            {
                var ue = new UTF8Encoding();
                var bytes = ue.GetBytes(strToEncryp);

                // encrypt bytes
                var md5 = new MD5CryptoServiceProvider();
                var sha = new SHA1CryptoServiceProvider();
                var hashBytes = sha.ComputeHash(bytes);

                // Convert the encrypted bytes back to a string (base 16)
                var hashString = "";

                foreach (byte t in hashBytes)
                {
                    hashString += Convert.ToString(t, 16).PadLeft(2, '0');
                }

                return hashString.PadLeft(32, '0');
            }
            return encryptString(strToEncryp);
        }
        public string encryptString(byte[] rawStringBytes)
        {

            var hashString = "";
            foreach (byte t in rawStringBytes)
            {
                hashString += Convert.ToString(t, 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');

        }
        public string Md5File(string filepath)
        {
            string hashString;
            using (var filestrm = new FileStream(filepath, FileMode.Open))
            {
                var md5Byte = new byte[filestrm.Length];

                filestrm.Read(md5Byte, 0, Convert.ToInt32(filestrm.Length.ToString()));
                var resultHash = HashByte(md5Byte);


                hashString = "";

                foreach (var t in resultHash)
                {
                    hashString += Convert.ToString(t, 16).PadLeft(2, '0');
                }
                filestrm.Close();
            }
            return hashString.PadLeft(32, '0');

        }




        public byte[] HashByte(byte[] bytes)
        {

            // encrypt bytes
            var md5 = new MD5CryptoServiceProvider();
            var hashBytes = md5.ComputeHash(bytes);
            // Convert the encrypted bytes back to a string (base 16)

            return hashBytes;
        }

        public byte[] HashByte(byte[] bytes, int algorithm)
        {
            byte[] hashBytes = null;

            if (algorithm == 0)//MD5
            {
                var md5 = new MD5CryptoServiceProvider();
                hashBytes = md5.ComputeHash(bytes);
            }
            else if (algorithm == 1)//SHA-1
            {
                var shs = new SHA1CryptoServiceProvider();
                hashBytes = shs.ComputeHash(bytes);
            }
            else
            {
                return null;
            }
            return hashBytes;
        }
    }
}
