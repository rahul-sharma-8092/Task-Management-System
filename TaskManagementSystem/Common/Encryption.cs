using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TaskManagementSystem.Common
{
    public static class Encryption
    {
        
        #region String Encrypt
        public static string Encrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            Encoding encoding = Encoding.UTF8;

            string secretKey = "Rahul$$$123";

            byte[] data = encoding.GetBytes(text);

            tripleDES.Key = md5.ComputeHash(encoding.GetBytes(secretKey));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateEncryptor();
            try
            {
                byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

                return Convert.ToBase64String(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while try to Encrypt");
            }
            finally
            {
                md5.Dispose();
                tripleDES.Dispose();
            }
        }
        #endregion

        #region String Decrypt
        public static string Decrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            Encoding encoding = Encoding.UTF8;

            string secretKey = "Rahul$$$123";

            byte[] dataToDecrypt = Convert.FromBase64String(text);

            tripleDES.Key = md5.ComputeHash(encoding.GetBytes(secretKey));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateDecryptor();
            try
            {
                byte[] result = transform.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);

                return encoding.GetString(result);
            }
            catch(Exception Ex)
            {
                throw new Exception("Error occured while try to Decrypt");
            }
            finally
            {
                md5.Dispose();
                tripleDES.Dispose();
            }
        }
        #endregion
    }
}