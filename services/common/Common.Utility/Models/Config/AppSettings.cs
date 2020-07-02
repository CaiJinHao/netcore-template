using Common.Models.Config.AppConfig;
using Common.Utility.Models.Config.AppConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config
{
    /// <summary>
    /// 应用程序配置文件对应
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 中间件扩展文件
        /// </summary>
        public MiddlewareExtensionFile MiddlewareExtension { get; set; }
        /// <summary>
        /// http请求记录器
        /// </summary>
        public HttpRequstRecordMiddlewareFile HttpRequstRecordMiddleware { get; set; }

        /// <summary>
        /// 服务容器扩展文件
        /// </summary>
        public ServiceCollectionExtensionFile ServiceCollectionExtension { get; set; }
        /// <summary>
        /// 验签配置
        /// </summary>
        public VerifySignatureFile VerifySignature { get; set; }
      
        /// <summary>
        /// 日志记录器配置
        /// </summary>
        public LoggerConfigModel LoggerConfig { get; set; }
        /// <summary>
        /// 系统文件路径
        /// </summary>
        public AppFilePathsModel AppFilePaths { get; set; }
        /// <summary>
        /// isc流媒体配置
        /// </summary>
        public IscLmtConfig IscLmtConfig { get; set; }
    }
}
