using System;
using System.Linq;
using System.Text;

namespace BLL_User.Extension
{
    public class SecurityExtension
    {
        public static string EncryptMD5(string sToEncrypt)
        {
            System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
            byte[] bytes = ue.GetBytes(sToEncrypt);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            StringBuilder hashString = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString.Append(hashBytes[i].ToString("x2"));
            }
            return hashString.ToString();
        }
    }
}
