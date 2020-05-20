using BuildSlnRename.DirectoryServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Xml;

namespace BuildSlnRename.Models
{
    public class AppSettings
    {
        /// <summary>
        /// 项目文件夹
        /// </summary>
        public string SourceDir { get; set; }
        /// <summary>
        /// 模板文件
        /// </summary>
        public ReplaceModel[] FileReplaceModels { get; set; }
        public ReplaceModel[] ContentReplaceModels { get; set; }
    }
}
