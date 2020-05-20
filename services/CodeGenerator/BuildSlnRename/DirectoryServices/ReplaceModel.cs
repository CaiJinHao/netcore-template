using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BuildSlnRename.DirectoryServices
{
    public  class ReplaceModel
    {
        public string SearchRegexStr { set { this.SearchRegex = new Regex(value); } }
        /// <summary>
        /// 搜索表达式
        /// </summary>
        public Regex SearchRegex { get; set; }
        /// <summary>
        /// 替换的新内容
        /// </summary>
        public string NewContent { get; set; }
    }
}
