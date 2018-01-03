using OnlineCourse.Core.Extentions.Encryption;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OnlineCourse.Core.Extentions
{
    public sealed class EncryptDecrypt
    {
        private const string _key1 = "abcdefghijklmnopqrstuvxywzABCDEFGHIJKLMNOPQRSTUVXYWZ1234567890";
        //private const string _key = "E546C8DF278CD5931069B522E695D4F2";
        private const string _key = "E546C8DF278CD5931069B522";


        public static string Encrypt(string textToEncript)
        {
            //return RijndaelEtM.Encrypt(textToEncript,_key, KeySize.Aes128);
            return RijndaelEtM.Encrypt(textToEncript, _key1, KeySize.Aes128);
        }

        public static string Decrypt(string encriptedToDecrypt)
        {
            return RijndaelEtM.Decrypt(encriptedToDecrypt, _key1, KeySize.Aes128);
        }

        public static string EncryptString(string text)
        {
            var key = Encoding.UTF8.GetBytes(_key);

            using (var aesAlg = TripleDES.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public static string DecryptString(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(_key);

            using (var aesAlg = TripleDES.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        public static string GetUrlHash(string combination)
        {
            using (var algorithm = SHA512.Create()) //or MD5 SHA256 etc.
            {
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(combination));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public static string GetUrlHash(int id, int publicId, byte kind)
        {
            using (var algorithm = SHA512.Create()) //or MD5 SHA256 etc.
            {
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(id.ToString() + publicId + kind));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

    }
}
