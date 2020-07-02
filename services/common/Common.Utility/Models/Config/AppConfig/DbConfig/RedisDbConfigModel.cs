using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.Config.AppConfig.DbConfig
{
    public class RedisDbConfigModel
    {
        public string ConnectionString { get; set; }
        public int ConnectTimeout { get; set; }
        public int SyncTimeout { get; set; }
        public string Password { get; set; }
    }
}
