using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.IdentityServer.Models
{
    public class AppSettingsModel
    {
        /// <summary>
        /// 验证数据URL
        /// </summary>
        public string VerifyUserUrl { get; set; }
        public ApiResourceConfigModel[] ApiResource { get; set; }
        public ClientConfigModel[] Clients { get; set; }
    }
}
