using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeGenerator.App.Models
{
    /// <summary>
    /// 表名重命名
    /// </summary>
    public class TableRenameModel
    {
        private string _searchRegexStr;
        /// <summary>
        /// 正则表达式字符串
        /// </summary>
        public string SearchRegexStr
        {
            get { return this._searchRegexStr; }
            set
            {
                this._searchRegexStr = value;
                if (!string.IsNullOrEmpty(value))
                {
                    this.SearchRegex = new Regex(value);
                }
            }
        }
        /// <summary>
        /// 搜索的正则表达式
        /// </summary>
        public Regex SearchRegex { get; set; }
        /// <summary>
        /// 新的内容
        /// </summary>
        public string NewContent { get; set; }
    }
}
