using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.App.Models
{
    public class ModelsTemplateFiles
    {
        /// <summary>
        /// Models的项目名称
        /// </summary>
        public string NameSpace { get; set; }
        public string DbModelTemplate { get; set; }
        /// <summary>
        /// DbModel文件后缀、类后缀
        /// </summary>
        public string DbModelFileSuffix { get; set; }
        public string RequestModelTemplate { get; set; }
        public string RequestModelFileSuffix { get; set; }
        public string ResponeModelTemplate { get; set; }
        public string ResponeModelFileSuffix { get; set; }
    }
}
