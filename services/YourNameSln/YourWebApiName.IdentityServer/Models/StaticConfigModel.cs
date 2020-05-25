using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.IdentityServer.Models
{
    public class StaticConfigModel
    {
        public static string ContentRootPath { get; set; }
        /// <summary>
        /// APP 配置实体
        /// </summary>
        public static AppSettingsModel AppSettings { get; set; }
    }
}
