using Common.Utility.Models.Config.AppConfig.DbConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config.AppConfig
{
    public class DbConnectionConfig
    {
        public MongoDBConfig MongoDB { get; set; }
        /// <summary>
        /// 连接字符串(MySql)
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
