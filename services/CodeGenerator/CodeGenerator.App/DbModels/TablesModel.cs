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
        private string _table_comment;
        public string table_comment {
            get { return _table_comment; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var _v = value.Replace("\r\n", " ");
                    var _i = _v.IndexOf('_') + 1;
                    _v = value.Substring(_i, value.Length - _i);
                    this._table_comment = _v;
                }
            }
        }
    }
}
