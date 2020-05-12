using Common.Utility.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DataBase.IDataBase
{
    /// <summary>
    /// 数据库上下文基类
    /// </summary>
    public abstract class DbContextAbstract
    {
        /// <summary>
        /// 获取表名
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <returns></returns>
        public string GetTableName<TTableModel>()
        {
            return ReflectHelper.GetValueByAttribute<TTableModel, TableAttribute>();
        }

        /// <summary>
        /// 获取主键
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <returns></returns>
        public string GetKeyName<TTableModel>()
        {
            var key = ReflectHelper.GetFieldsByAttribute<TTableModel, KeyAttribute>().FirstOrDefault();
            if (key == null)
            {
                throw new Exception("没有定义主键字段");
            }
            return key;
        }

        /// <summary>
        /// 获取所有字段
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <returns></returns>
        public IEnumerable<string> GetFields<TTableModel>(List<string> notInFields = null)
        {
            var fields = ReflectHelper.GetFieldsByAttribute<TTableModel>();
            if (notInFields != null && notInFields.Count > 0)
            {
                return fields.Where(a => !notInFields.Contains(a));
            }
            return fields;
        }
    }
}
