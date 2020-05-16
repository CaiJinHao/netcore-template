using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Xml;

namespace CodeGenerator.App.Models
{
    public class AppSettings
    {
        /// <summary>
        /// 模板文件
        /// </summary>
        public TemplateModel Template { get; set; }
        public DbConnectionModel DbConnection { get; set; }
    }
}
