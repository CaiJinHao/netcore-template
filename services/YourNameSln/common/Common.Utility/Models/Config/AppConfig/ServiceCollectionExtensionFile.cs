using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config.AppConfig
{
    public class ServiceCollectionExtensionFile
    {
        /// <summary>
        /// 请求策略配置
        /// </summary>
        public CorsConfig Cors { get; set; }

        private SwaggerDocConfig[] _swaggerDoc;
        /// <summary>
        /// API 文档 可以读数组 但要数组对象上级的节点
        /// </summary>
        public SwaggerDocConfig[] SwaggerDoc {
            get { return this._swaggerDoc; }
            set { this._swaggerDoc = value; }
        }
        /// <summary>
        /// 身份验证
        /// </summary>
        public IdentityJwtConfig IdentityJwt { get; set; }
   
        /// <summary>
        /// 数据库连接配置
        /// </summary>
        public DbConnectionConfig DbConnection { get; set; }
        /// <summary>
        /// 后台任务配置
        /// </summary>
        public BackgroundTasksConfig BackgroundTasks { get; set; }
    }
}
