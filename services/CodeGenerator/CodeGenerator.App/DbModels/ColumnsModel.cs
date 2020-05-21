﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.App.DbModels
{
    public class ColumnsModel
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string column_name { get; set; }
        /// <summary>
        /// 字段排序
        /// </summary>
        public int? ordinal_position { get; set; }
        private string _nullable;
        /// <summary>
        /// 是否可为null
        /// </summary>
        public string is_nullable { get => _nullable;
            set {
                _nullable = value;
                if (value=="YES")
                {
                    nullable = true;
                }
            }
        }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string data_type { get; set; }
        /// <summary>
        /// 最大长度
        /// </summary>
        public Int64? character_maximum_length { get; set; }
        private string col_key;
        /// <summary>
        /// 主键标识列
        /// </summary>
        public string column_key { get => col_key;
            set {
                col_key = value;
                if (value== "PRI")
                {
                    primary_key = true;
                }
            }
        }
        /// <summary>
        /// 列注释
        /// </summary>
        public string column_comment { get; set; }
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool primary_key { get; set; }
        /// <summary>
        /// 是否可为null
        /// </summary>
        public bool nullable { get; set; }
    }
}
