using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.App.Models
{
/*
  {0}代表表名
  _区分单词 用来转为驼峰命名
  DependencyInjection 这里不要写接口，写继承接口的类就可以
*/
    public class TemplateFilesModel
    {
        /// <summary>
        /// 文件名称模板
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件存放的文件夹
        /// </summary>
        public string FileDirName { get; set; }
        /// <summary>
        /// 文件后缀
        /// </summary>
        public string FileSuf{ get;set; }
        /// <summary>
        /// 模板文件 去除Template 就是文件夹名称
        /// </summary>
        public string TemplateFile { get; set; }

        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string FileSavePath { get; set; }

    }
}
