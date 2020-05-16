using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.App.DbModels
{
    public class TablesModel
    {
        /// <summary>
        /// 表名 文件名
        /// </summary>
        public string table_name { get; set; }
        public string table_comment { get; set; }
    }
}
