using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.IdentityServer.Models
{
    public class ApiResourceConfigModel
    {
        /// <summary>
        /// API 资源名称
        /// </summary>
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
