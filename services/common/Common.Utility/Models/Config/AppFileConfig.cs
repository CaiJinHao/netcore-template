using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config
{
    /// <summary>
    /// 系统文件路径配置
    /// </summary>
    public class AppFileConfig
    {
        /// <summary>
        /// 私匙
        /// </summary>
        public static string PrivateKey = "AppData/RsaKey/privatekey";
        /// <summary>
        /// 公匙
        /// </summary>
        public static string PublickKey = "AppData/RsaKey/publickkey";
    }
}
