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
        /// 模板文件
        /// </summary>
        public TemplateFilesModel[] TemplateFiles { get; set; }
    }
}
