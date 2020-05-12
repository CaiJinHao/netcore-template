using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config.AppConfig
{
    /// <summary>
    /// 身份验证服务配置
    /// </summary>
    public class IdentityJwtConfig
    {
        public JwtBearerConfig JwtBearer { get; set; }
        public PasswordTokenConfig PasswordToken { get; set; }
    }
}
