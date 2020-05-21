using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.App.Models
{
    public class DbConnectionModel
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string MySqlConnection { get; set; }
        /// <summary>
        /// 连接字符
        /// </summary>
        public string SqlServerConnection { get; set; }
        /// <summary>
        /// 数据库名称用于查询字段和表
        /// </summary>
        public string DbName { get; set; }
    }
}
