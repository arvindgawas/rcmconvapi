using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Common
{
    public static class Helper
    {
        /// <summary>
        /// Encrypts the specified text using the specified key
        /// </summary>
        /// <param name="textToEncrypt">Text to be encrypted</param>
        /// <param name="encryptionKey">Encyption key to be used</param>
        /// <returns>Encrypted text,if successfull, blank as otherwise</returns>
        public static string EncryptText(string textToEncrypt, string encryptionKey)
        {
            string encryptedText = "";

            try
            {
                byte[] clearBytes = Encoding.Unicode.GetBytes(textToEncrypt);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        encryptedText = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception)
            {
                //Do not throw;
            }

            return encryptedText;
        }

        /// <summary>
        /// Decrypts the specified text using the specified key
        /// </summary>
        /// <param name="textToDecrypt">Text to be decrypted</param>
        /// <param name="encryptionKey">Encryption key</param>
        /// <returns>Decrypted text,if successful, blank as otherwise</returns>
        public static string DecryptText(string textToDecrypt, string encryptionKey)
        {
            string decryptedText = string.Empty;
            try
            {
                byte[] clearBytes = Convert.FromBase64String(textToDecrypt);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        decryptedText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception)
            {
                ////Do not throw;
            }

            return decryptedText;
        }

    }
}





