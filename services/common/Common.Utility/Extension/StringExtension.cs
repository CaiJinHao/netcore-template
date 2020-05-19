using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Extension
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 十六进制字符串转字节数组
        /// </summary>
        /// <param name="InString"></param>
        /// <returns></returns>
        public static byte[] ConvertBytes(this string InString)
        {
            string[] strArray = InString.Trim().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            byte[] buffer = new byte[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                string str = strArray[i];
                buffer[i] = Convert.ToByte(str, 16);
            }
            return buffer;
        }
    }
}
