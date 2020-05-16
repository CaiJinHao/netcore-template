using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config.AppConfig
{
    /// <summary>
    /// 日志记录器配置
    /// </summary>
    public class LoggerConfigModel
    {
        public bool EnableDebug { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnableInfo { get; set; }
    }
}
