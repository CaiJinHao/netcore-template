using Common.Utility.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DataBase
{
    /// <summary>
    /// 数据Table 工具
    /// </summary>
    public class DbContextAbstract
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

        /// <summary>
        /// 根据Model生成查询SQL
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetSqlQueryString<TModel>(TModel model)
        {
            var sqlWhere = new StringBuilder();//查询条件
            var filedsInfo = model.GetType().GetProperties();
            foreach (var _field in filedsInfo)
            {
                var v = _field.GetValue(model, null);
                if (v != null)
                {
                    var fieldType = v.GetType();
                    switch (fieldType.Name)
                    {
                        case "String":
                            {
                                if (!string.IsNullOrEmpty((string)v))
                                {
                                    sqlWhere.Append($" AND b1.{_field.Name} LIKE @{_field.Name}");
                                }
                            }
                            break;
                        case "Int16":
                        case "Int32":
                        case "Int64":
                            {
                                var _value = (Int64)v;
                                if (_value > 0)
                                {
                                    sqlWhere.Append($" AND b1.{_field.Name} = @{_field.Name}");
                                }
                            }
                            break;
                        default:
                            throw new Exception("没有匹配的类型");
                    }
                }
            }
            return sqlWhere.ToString();
        }

        /// <summary>
        /// 根据Model生成更新的SQL
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetSqlUpdateString<TModel>(TModel model)
        {
            var sqlWhere = new StringBuilder();//查询条件
            var filedsInfo = model.GetType().GetProperties();
            foreach (var _field in filedsInfo)
            {
                var v = _field.GetValue(model, null);
                if (v != null)
                {
                    var fieldType = v.GetType();
                    switch (fieldType.Name)
                    {
                        case "String":
                            {
                                if (!string.IsNullOrEmpty((string)v))
                                {
                                    sqlWhere.Append($" {_field.Name} = @{_field.Name},");
                                }
                            }
                            break;
                        case "Int16":
                        case "Int32":
                        case "Int64":
                            {
                                var _value = (Int64)v;
                                if (_value > 0)
                                {
                                    sqlWhere.Append($" {_field.Name} = @{_field.Name},");
                                }
                            }
                            break;
                        //datetime,
                        default:
                            throw new Exception("没有匹配的类型");
                    }
                }
            }
            return sqlWhere.ToString();
        }
    }
}
