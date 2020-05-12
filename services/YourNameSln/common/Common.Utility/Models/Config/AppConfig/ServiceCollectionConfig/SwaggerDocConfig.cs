using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config.AppConfig
{
    /// <summary>
    /// API 文档配置
    /// </summary>
    public class SwaggerDocConfig
    {
        public string ApiVersion { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
