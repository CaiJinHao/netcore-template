using Common.Utility.Models.AppConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config
{
    /// <summary>
    /// 全局ALL配置
    /// </summary>
    public class StaticConfig
    {
        /// <summary>
        /// Env.ContentRootPath 项目目录 启动时赋值
        /// </summary>
        public static string ContentRootPath { get; set; }
        /// <summary>
        /// APP 配置实体
        /// </summary>
        public static AppSettings AppSettings { get; set; }
        /// <summary>
        /// 预警配置实体
        /// </summary>
        public static WarningConfigModel WarningConfigModel { get; set; }
    }
}
