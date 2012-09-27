// <copyright file="Cryptography.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  Cryptography class provide method to encrypt and decrypt connection string
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.16">Created</change>
// </history>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Fanex.Data
{
    public static class Cryptography
    {
        #region Const Params
        private const string Chars = "qwertyuiopasdfghjklzxcvbnm0123456789";
        private const string CharsNoZero = "qwertyuipasdfghjklzxcvbnm123456789";
        private const string Numbers = "0123456789";
        private static readonly DESCryptoServiceProvider CryptoServiceProvider = new DESCryptoServiceProvider();
        private static readonly byte[] RgbKey = new byte[] { 0x21, 0x63, 0x54, 0x5a, 0x91, 0xac, 110, 0xcc };
        private static readonly byte[] RgbIv = Encoding.UTF8.GetBytes("&％#@?,:*!~^.");
        #endregion

        #region Public functions
        public static string CfsEnCrypt(string plainText)
        {
            var encryptedText = string.Empty;
            if (!string.IsNullOrWhiteSpace(plainText))
            {
                var buffer = Encoding.UTF8.GetBytes(plainText);
                using (var stream = CreateMemoryStream(buffer))
                {
                    encryptedText = Convert.ToBase64String(stream.ToArray());
                }
            }

            return encryptedText;
        }

        /// <summary>
        /// Decrypt a non-empty encrypted string
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        public static string CfsDeCrypt(string encryptedText)
        {
            var decryptedText = string.Empty;

            if (!string.IsNullOrWhiteSpace(encryptedText))
            {
                var buffer = Convert.FromBase64String(encryptedText);
                using (var stream = CreateMemoryStream(buffer))
                {
                    decryptedText = Encoding.UTF8.GetString(stream.ToArray());
                }
            }

            return decryptedText;
        }

        public static string DeCryptConnectionString(string connStr)
        {
            if (string.IsNullOrWhiteSpace(connStr))
                throw new ArgumentException(connStr);

            var decryptPwd = GetDecryptPwd(connStr, "=");
            var str = CfsDeCrypt(decryptPwd[1].Trim());
            return (decryptPwd[0].Trim() + str + ";");
        }
        #endregion

        #region private functions

        private static MemoryStream CreateMemoryStream(byte[] buffer)
        {
            var stream = new MemoryStream();
            var stream2 = new CryptoStream(stream, CryptoServiceProvider.CreateDecryptor(RgbKey, RgbIv)
                , CryptoStreamMode.Write);

            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            return stream;
        }



        private static string[] GetDecryptPwd(string sourceString, string searchString)
        {
            int length = 1;
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4;

            for (int i = sourceString.Length - 1; i >= 0; i--)
            {
                str = str + sourceString.Substring(i, 1);
            }
            for (int j = searchString.Length - 1; j >= 0; j--)
            {
                str2 = str2 + searchString.Substring(j, 1);
            }

            if (str.Substring(1, 2) != "==")
            {
                if (str.Substring(1, 1) != "=")
                {
                    for (int n = 1; n <= str.Length; n++)
                    {
                        if (str.Substring(n, 1) == "=")
                        {
                            break;
                        }
                        length++;
                    }
                    str4 = str.Substring(0, length);
                    for (int num9 = str4.Length; num9 > 1; num9--)
                    {
                        str3 = str3 + str4.Substring(num9 - 1, 1);
                    }

                    return new[] { sourceString.Substring(0, sourceString.Length - length), str3 };
                }

                length++;
                for (int m = 2; m <= str.Length; m++)
                {
                    if (str.Substring(m, 1) == "=")
                    {
                        break;
                    }
                    length++;
                }
            }
            else
            {
                length += 2;
                for (int num4 = 3; num4 <= str.Length; num4++)
                {
                    if (str.Substring(num4, 1) == "=")
                    {
                        break;
                    }
                    length++;
                }
                str4 = str.Substring(0, length);
                for (int num5 = str4.Length; num5 > 1; num5--)
                {
                    str3 = str3 + str4.Substring(num5 - 1, 1);
                }
                return new[] { sourceString.Substring(0, sourceString.Length - length), str3 };
            }


            str4 = str.Substring(0, length);
            for (int k = str4.Length; k > 1; k--)
            {
                str3 = str3 + str4.Substring(k - 1, 1);
            }

            return new[] { sourceString.Substring(0, sourceString.Length - length), str3 };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="noZeroO"></param>
        /// <returns></returns>
        private static string RandomString(int length, bool noZeroO = false)
        {
            string chars = noZeroO
                               ? Chars
                               : CharsNoZero;

            string rndStr = string.Empty;
            Random rnd = new Random(DateTime.Now.Second + DateTime.Now.Millisecond);
            int len = chars.Length;

            for (int i = 0; i < length; ++i)
            {
                rndStr += chars[rnd.Next() % len];
            }
            return rndStr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns>a string of a sequence number</returns>
        private static string RandomStringNumber(int length)
        {
            string rndStr = string.Empty;
            Random rnd = new Random();
            int len = Numbers.Length;

            for (int i = 0; i < length; ++i)
            {
                rndStr += Numbers[rnd.Next() % len];
            }
            return rndStr;
        }

        private static string RemoveSpecialChars(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            return str.Replace("\n", "\\n").Replace("\"", "\\\"").Replace("'", "\\'");
        }
        #endregion
    }
}
