﻿using IDataBase.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace IDataBase.DbExtensions
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
        public IEnumerable<string> GetKeyName<TTableModel>()
        {
            return ReflectHelper.GetFieldsByAttribute<TTableModel, KeyAttribute>();
        }

        /// <summary>
        /// 获取所有字段
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <returns></returns>
        public IEnumerable<string> GetFields<TTableModel>(string[] notInFields = null)
        {
            var fields = ReflectHelper.GetFieldsByAttribute<TTableModel>();
            if (notInFields != null && notInFields.Length > 0)
            {
                return fields.Where(a => !notInFields.Contains(a));
            }
            return fields;
        }

        /// <summary>
        /// 获取用","号隔开的字段
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <param name="fieldTableName">字段表别名前缀</param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public string GetFieldsToString<TTableModel>(string fieldTableName,IEnumerable<string> fields = null)
        {
            var isAutoFields = false;
            if (fields == null)
            {
                fields = GetFields<TTableModel>();
                isAutoFields = true;
            }
            if (string.IsNullOrEmpty(fieldTableName))
            {
                //注意每个数据库的标识方法不一样 SQL SERVER []/MYSQL ``
                if (isAutoFields)
                {
                    return string.Join(",", fields.Select(a => $"[{a}]"));
                }
                else
                {
                    return string.Join(",", fields);//为了支持case when 等其他语句
                }
            }
            else
            {
                return string.Join(",", fields.Select(a => $"{fieldTableName}.[{a}]"));
            }
        }

        /// <summary>
        /// 根据Model生成查询SQL
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetSqlQueryString<TModel>(TModel model, string[] notInFields = null, string fieldPrefix = "b1.")
        {
            var sqlWhere = new StringBuilder();//查询条件

            Action<System.Reflection.PropertyInfo> appendWhere = (_field) =>
            {
                sqlWhere.Append($" AND {fieldPrefix}{_field.Name} = @{_field.Name}");
            };

            var fieldsInfo = typeof(TModel).GetProperties();
            if (notInFields != null)
            {
                fieldsInfo = fieldsInfo.Where(a => !notInFields.Contains(a.Name)).ToArray();//不包含字段集合的字段
            }
            foreach (var _field in fieldsInfo)
            {
                var v = _field.GetValue(model, null);
                if (v != null)
                {
                    var fieldType = v.GetType();
                    switch (fieldType.Name)
                    {
                        case "String":
                            {
                                var _v = (string)v;
                                if (!string.IsNullOrEmpty(_v))
                                {
                                    var myOperator = "=";
                                    if (_v.Contains("%"))
                                    {
                                        myOperator = "LIKE";
                                    }
                                    sqlWhere.Append($" AND {fieldPrefix}{_field.Name} {myOperator} @{_field.Name}");
                                }
                            }
                            break;
                        case "Int16":
                            {
                                var _value = (Int16)v;
                                if (_value > 0)
                                {
                                    appendWhere(_field);
                                }
                            }
                            break;
                        case "Int32":
                            {
                                var _value = (Int32)v;
                                if (_value > 0)
                                {
                                    appendWhere(_field);
                                }
                            }
                            break;
                        case "Int64":
                            {
                                var _value = (Int64)v;
                                if (_value > 0)
                                {
                                    appendWhere(_field);
                                }
                            }
                            break;
                        case "Decimal":
                            {
                                var _value = (decimal)v;
                                if (_value > 0)
                                {
                                    appendWhere(_field);
                                }
                            }
                            break;
                        case "DateTime": 
                        case "Boolean":
                        default:
                            break;
                            //throw new Exception("没有匹配的类型");
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
        /// <param name="notInFields">无法更新的字段类型可置位可空类型如:Bool</param>
        /// <param name="notValidateFields">不需要验证值得字段</param>
        /// <returns></returns>
        public string GetSqlUpdateString<TModel>(TModel model, string[] notInFields = null,string[] notValidateFields=null)
        {
            var sqlWhere = new StringBuilder();//查询条件
            Action<System.Reflection.PropertyInfo> appendField = (_field) =>
            {
                sqlWhere.Append($" {_field.Name} = @{_field.Name},");
            };
            var fieldsInfo = model.GetType().GetProperties();
            if (notInFields != null)
            {
                fieldsInfo = fieldsInfo.Where(a => !notInFields.Contains(a.Name)).ToArray();//不包含字段集合的字段
            }
            foreach (var _field in fieldsInfo)
            {
                var v = _field.GetValue(model, null);
                if (v != null)
                {
                    var fieldType = v.GetType();
                    if (notValidateFields != null && notValidateFields.Contains(fieldType.Name))
                    {//不需要验证值的字段
                        appendField(_field);
                    }
                    else
                    {
                        if (fieldType.IsEnum)
                        {
                            if (((Int32)v) > 0)
                            {
                                appendField(_field);
                            }
                        }
                        else
                        {
                            switch (fieldType.Name)
                            {
                                case "String":
                                    {
                                        if (!string.IsNullOrEmpty((string)v))
                                        {
                                            appendField(_field);
                                        }
                                    }
                                    break;
                                case "DateTime":
                                    {
                                        var _value = (DateTime)v;
                                        if (_value > new DateTime(1900, 1, 1))
                                        {
                                            appendField(_field);
                                        }
                                    }
                                    break;
                                case "Int16":
                                    {
                                        var _value = (Int16)v;
                                        if (_value > 0)
                                        {
                                            appendField(_field);
                                        }
                                    }
                                    break;
                                case "Int32":
                                    {
                                        var _value = (Int32)v;
                                        if (_value > 0)
                                        {
                                            appendField(_field);
                                        }
                                    }
                                    break;
                                case "Int64":
                                    {
                                        var _value = (Int64)v;
                                        if (_value > 0)
                                        {
                                            appendField(_field);
                                        }
                                    }
                                    break;
                                case "Decimal":
                                    {
                                        var _value = (decimal)v;
                                        if (_value > 0)
                                        {
                                            appendField(_field);
                                        }
                                    }
                                    break;
                                case "Boolean":
                                    {
                                        appendField(_field);
                                    }
                                    break;
                                default:
                                    //appendField(_field);
                                    //break;
                                    throw new Exception($"没有匹配的类型:{fieldType.Name}");//Bool类型值需要排除字段
                            }
                        }
                    }
                }
            }
            return sqlWhere.ToString().Trim(',');
        }
    }
}
