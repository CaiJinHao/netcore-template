using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config.AppConfig
{
    /// <summary>
    /// 验证签名文件配置
    /// </summary>
    public class VerifySignatureFile
    {
        /// <summary>
        /// 公匙
        /// </summary>
        public string PublicKeyFile { get; set; }
        /// <summary>
        /// 签名过期时间 单位：秒
        /// </summary>
        public int ExpirationTime { get; set; }
    }
}
