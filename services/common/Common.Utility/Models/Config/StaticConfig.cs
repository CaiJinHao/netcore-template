using Common.Utility.Models.AppConfig;
using Snowflake.Core;
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
        /// <summary>
        /// 超级管理员角色ID
        /// </summary>
        public const string SuperadminRoleId = "-1";
        private static IdWorker idWorker;
        /// <summary>
        /// 获取唯一ID
        /// </summary>
        /// <returns></returns>
        public static long GetId()
        {
            if (idWorker==null)
            {
                idWorker = new IdWorker(1, 1);
            }
            return idWorker.NextId();
        }
    }
}
