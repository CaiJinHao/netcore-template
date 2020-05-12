using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config.AppConfig
{
    public class JwtBearerConfig
    {
        /// <summary>
        /// 授权服务地址
        /// </summary>
        public string Authority { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        /// <summary>
        /// API 资源名称(ApiResource Name)
        /// </summary>
        public string Audience { get; set; }
    }
}
