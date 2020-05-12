using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudBox.App.Extensions
{
    /// <summary>
    /// 配置文件集合
    /// </summary>
    public class ConfigurationsModel
    {
        /// <summary>
        /// 应用程序配置文件
        /// </summary>
        public static readonly string AppSettings = "appsettings.json";
        /// <summary>
        /// WEB 主机启动配置
        /// </summary>
        public static readonly string HostSettings = "Configurations/hostsettings.json";
        /// <summary>
        /// Log4net 配置
        /// </summary>
        public static readonly string Log4netConfig = "Configurations/log4net.config";
        ///// <summary>
        ///// consul 配置
        ///// </summary>
        //public static readonly string ConsulSettings = "Configurations/consulsettings.json";
    }
}
