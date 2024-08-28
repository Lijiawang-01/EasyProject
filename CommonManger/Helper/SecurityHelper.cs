using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonManager.Helper
{
    public class SecurityHelper
    {
        public static string QuotePriceKey = "easy_QuotePriceEncryption";

        public static readonly string publicKey = "easy_CookieEncryption";
        public static string SafeHtmlFragment(string sourceStr, bool check = true)
        {
            if (check)
            {
                return sourceStr.Replace("<", "＜").Replace(">", "＞").Replace("\"", "＼")
                    .Replace("%", "％")
                    .Replace(";", "；")
                    .Replace("(", "（")
                    .Replace(")", "）")
                    .Replace("&", "＆")
                    .Replace("+", "＋");
            }

            return sourceStr;
        }
        /// <summary>
        /// Sha1签名
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string Sha1Signature(string str, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            var buffer = encoding.GetBytes(str);
            var data = SHA1.Create().ComputeHash(buffer);
            StringBuilder sub = new StringBuilder();
            foreach (var t in data)
            {
                sub.Append(t.ToString("x2"));
            }

            return sub.ToString();
        }
        public static string UrlEncrypt(string url, bool isVery = true)
        {
            string text = "3E2DC7AC-32B6-4574-9BD2-A8B242D31AA8";
            string text2 = (isVery ? "0" : "1");
            string result = url;
            if (!url.Contains("?k=") && !url.Contains("&k="))
            {
                if (url.IndexOf('?') > 0)
                {
                    if (!isVery)
                    {
                        string text3 = url.Substring(0, url.IndexOf('?'));
                        text3 = text3.Substring(text3.LastIndexOf('/') + 1);
                        string encryptValue = "u=" + text3 + "&v=" + text2;
                        string str = EncryptString(encryptValue, publicKey);
                        str = HttpUtility.UrlEncode(str);
                        result = url + "&k=" + str;
                    }
                    else
                    {
                        string encryptValue2 = url.Substring(url.IndexOf('?') + 1) + "&t=" + text + "&v=" + text2;
                        string str2 = EncryptString(encryptValue2, publicKey);
                        str2 = HttpUtility.UrlEncode(str2);
                        result = url.Substring(0, url.IndexOf('?') + 1) + "k=" + str2;
                    }
                }
                else
                {
                    int result2 = -1;
                    url = url.TrimEnd('/');
                    string s = url.Substring(url.LastIndexOf('/') + 1);
                    if (int.TryParse(s, out result2))
                    {
                        if (isVery)
                        {
                            string str3 = EncryptString("id=" + result2 + "&t=" + text + "&v=" + text2, publicKey);
                            str3 = HttpUtility.UrlEncode(str3);
                            result = url.Substring(0, url.LastIndexOf('/')) + "?k=" + str3;
                        }
                        else
                        {
                            string text4 = url.Substring(0, url.LastIndexOf('/'));
                            text4 = text4.Substring(text4.LastIndexOf('/') + 1);
                            string str4 = EncryptString("u=" + text4 + "&v=" + text2, publicKey);
                            str4 = HttpUtility.UrlEncode(str4);
                            result = url.Substring(0, url.LastIndexOf('/')) + "?id=" + result2 + "&k=" + str4;
                        }
                    }
                    else if (!isVery)
                    {
                        string str5 = EncryptString("u=" + url.Substring(url.LastIndexOf('/') + 1) + "&v=" + text2, publicKey);
                        str5 = HttpUtility.UrlEncode(str5);
                        result = url + "?k=" + str5;
                    }
                }
            }

            return result;
        }

        public static string EncryptString(string encryptValue, string key)
        {
            string text = "加密出错!";
            SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("TripleDES");
            symmetricAlgorithm.Mode = CipherMode.ECB;
            symmetricAlgorithm.Key = Encoding.UTF8.GetBytes(SplitStringLen(key, 24, '0'));
            symmetricAlgorithm.Padding = PaddingMode.PKCS7;
            ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(encryptValue);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
            try
            {
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.FlushFinalBlock();
                text = Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                text = ex.ToString();
            }
            finally
            {
                cryptoStream.Close();
                cryptoStream.Dispose();
                memoryStream.Close();
                memoryStream.Dispose();
                symmetricAlgorithm.Clear();
                cryptoTransform.Dispose();
            }

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static string SplitStringLen(string s, int len, char b)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }

            if (s.Length >= len)
            {
                return s.Substring(0, len);
            }
            return s.PadLeft(len, b);
        }

        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(Text);
            MD5 mD = MD5.Create();
            byte[] array = mD.ComputeHash(Encoding.UTF8.GetBytes(sKey));
            string text = "";
            for (int i = 0; i < array.Length; i++)
            {
                text += array[i].ToString("x2").ToUpperInvariant();
            }

            dESCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(text.Substring(0, 8));
            dESCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(text.Substring(0, 8));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder stringBuilder = new StringBuilder();
            byte[] array2 = memoryStream.ToArray();
            foreach (byte b in array2)
            {
                stringBuilder.AppendFormat("{0:X2}", b);
            }

            return stringBuilder.ToString();
        }

        public static string QuoteEncryptString(string encryptValue, string key, bool base64Flag = true)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(encryptValue);
            TripleDES tripleDES = TripleDES.Create();
            byte[] bytes2 = Encoding.UTF8.GetBytes(SplitStringLen(key, 24, '0'));
            byte[] array = new byte[24];
            tripleDES.Key = bytes2;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cryptoTransform = tripleDES.CreateEncryptor();
            byte[] array2 = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
            string text = "";
            if (base64Flag)
            {
                return Convert.ToBase64String(array2, 0, array2.Length);
            }

            text = BitConverter.ToString(array2);
            return text.Replace("-", "");
        }

        public static string QuoteDecryptString(string decryptString, string key, bool base64Flag = true)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(SplitStringLen(key, 24, '0'));
            TripleDES tripleDES = TripleDES.Create();
            tripleDES.Key = bytes;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cryptoTransform = tripleDES.CreateDecryptor();
            if (base64Flag)
            {
                byte[] array = Convert.FromBase64String(decryptString);
                byte[] array2 = new byte[24];
                byte[] bytes2 = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
                return Encoding.UTF8.GetString(bytes2);
            }

            decryptString = RecoverDecryptString(decryptString);
            string[] array3 = decryptString.Split("-".ToCharArray());
            byte[] array4 = new byte[array3.Length];
            for (int i = 0; i < array3.Length; i++)
            {
                array4[i] = byte.Parse(array3[i], NumberStyles.HexNumber);
            }

            byte[] bytes3 = cryptoTransform.TransformFinalBlock(array4, 0, array4.Length);
            return Encoding.UTF8.GetString(bytes3);
        }

        private static string RecoverDecryptString(string machineCode)
        {
            string text = "";
            int num = 1;
            for (int i = 0; i < machineCode.Length; i++)
            {
                text += machineCode[i];
                if (num % 2 == 0)
                {
                    text += "-";
                }

                num++;
            }

            return text.TrimEnd('-');
        }
    }
}
