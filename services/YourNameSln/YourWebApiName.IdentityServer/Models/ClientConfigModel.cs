using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.IdentityServer.Models
{
    public class ClientConfigModel
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string AllowedGrantTypes { get; set; }
        public int AccessTokenLifetime { get; set; }
        public string ClientSecrets { get; set; }
        /// <summary>
        /// api名称
        /// </summary>
        public string ApiName { get; set; }
    }
}
