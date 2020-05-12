using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common.Utility.Encryption
{
    /// <summary>
    /// 字符串加密扩展
    /// </summary>
    public static class StrEncrypt
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncryptToMD5(this string str)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(str);//将字符编码为一个字节序列
            byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值
            md5.Clear();
            string rstr = "";
            for (int i = 0; i < md5data.Length - 1; i++)
            {
                rstr += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return rstr;
        }
    }
}
