using Common.Models.Config.AppConfig.DbConfig;
using Common.Utility.Models.Config.AppConfig.DbConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config.AppConfig
{
    public class DbConnectionConfig
    {
        /// <summary>
        /// reset api 请求SQL时间调试
        /// </summary>
        public bool MiniProfiler { get; set; }
        /// <summary>
        /// SQL执行超时最长时间 秒
        /// </summary>
        public int CommandTimeOut { get; set; }
        public MongoDBConfig MongoDB { get; set; }
        /// <summary>
        /// 默认连接的数据库
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 第二个数据库
        /// </summary>
        public string ConnectionStringDb1 { get; set; }
        /// <summary>
        /// Redis
        /// </summary>
        public RedisDbConfigModel RedisDb { get; set; }
    }
}
