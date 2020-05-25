using CodeGenerator.App.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.App.Models
{
    public class TemplateModel
    {
        /// <summary>
        /// 模板生成的文件保存的路径
        /// </summary>
        public string SaveFilesPath { get; set; }
        /// <summary>
        /// 程序命名空间
        /// </summary>
        public string NameSpace { get; set; }
        /// <summary>
        /// 表、字段命名格式
        /// </summary>
        public EnumNamingFormat NamingFormat { get; set; }
        /// <summary>
        /// API 版本
        /// </summary>
        public string ApiVersion { get; set; }
        /// <summary>
        /// API 控制器的命名空间
        /// </summary>
        public string ApiControllerNameSpace { get; set; }
        /// <summary>
        /// 模板文件
        /// </summary>
        public TemplateFilesModel[] TemplateFiles { get; set; }
        /// <summary>
        /// 表名称重命名
        /// </summary>
        public TableRenameModel[] TableRename { get; set; }
        /// <summary>
        /// 过滤表SQL 
        /// </summary>
        public string TableSqlWhere { get; set; }
    }
}
