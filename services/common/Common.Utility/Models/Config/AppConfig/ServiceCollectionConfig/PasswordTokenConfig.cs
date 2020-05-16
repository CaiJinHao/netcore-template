using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config.AppConfig
{
    public class PasswordTokenConfig
    {
        /// <summary>
        /// 授权TOKEN地址
        /// </summary>
        public string Address { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        /// <summary>
        /// Client 有权限的 Scopes
        /// </summary>
        public string Scope { get; set; }
    }
}
