using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config.AppConfig
{
    /// <summary>
    /// 策略配置
    /// </summary>
    public class CorsConfig
    {
        public CorsPolicyConfig[] Policy { get; set; }
    }

    /// <summary>
    /// 跨域策略配置
    /// </summary>
    public class CorsPolicyConfig
    {
        public string Name { get; set; }
        public string Origins { get; set; }
    }
}
