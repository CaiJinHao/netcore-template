using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using MongoDB.Bson;

namespace Common.Utility.Other
{
    /// <summary>
    /// 反射
    /// </summary>
    public class ReflectHelper
    {
        /// <summary>
        /// 将对象转为key/value键对值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> ConvertToDictionary<T>(T model)
        {
            var dic = new Dictionary<string, dynamic>();
            var properties = model.GetType().GetProperties();
            foreach (var item in properties)
            {
                var _v = item.GetValue(model);
                if (!PropertyIsNull(item.PropertyType, _v))
                {
                    dic.Add(item.Name, _v);
                }
            }
            return dic;
        }

        /// <summary>
        /// 不包含符合正则的字段，不包含没有值得字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="notInFileds"></param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> ConvertToDictionary<T>(T model,string notRegex)
        {
            var dic = new Dictionary<string, dynamic>();
            var properties = model.GetType().GetProperties();
            foreach (var item in properties)
            {
                var _t = new Regex(notRegex).IsMatch(item.Name);
                if (!_t)
                {
                    var _v = item.GetValue(model);
                    if (!PropertyIsNull(item.PropertyType, _v))
                    {
                        dic.Add(item.Name, _v);
                    }
                }
            }
            return dic;
        }

        /// <summary>
        /// 判断属性是否为null
        /// </summary>
        /// <param name="PropertyType"></param>
        /// <param name="_v"></param>
        /// <returns></returns>
        private static bool PropertyIsNull(Type PropertyType,object _v)
        {
            if (PropertyType == typeof(string))
            {
                return string.IsNullOrEmpty((string)_v);
            }
            else if (PropertyType.IsEnum)
            {
                return (Convert.ToInt64(_v)) <= 0;
            }
            else if (PropertyType == typeof(DateTime))
            {
                return (DateTime)_v < new DateTime(1900, 1, 1);
            }
            else if (PropertyType == typeof(ObjectId))
            {
                return string.IsNullOrEmpty(_v.ToString().Replace("0", ""));
            }
            return false;
        }

        /// <summary>
        /// 获取包含某一特性的字段集合
        /// </summary>
        /// <typeparam name="TTableModel">model</typeparam>
        /// <typeparam name="TAttribute">特性</typeparam>
        /// <returns></returns>
        public static IEnumerable<string> GetFieldsByAttribute<TTableModel, TAttribute>() where TAttribute:Attribute
        {
            return typeof(TTableModel).GetProperties()
                .Where(prop=> prop.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault()!=null)
                .Select(a=>a.Name);

            /*等价于 var fields = new List<string>();
            var properties = typeof(TTableModel).GetProperties();
            foreach (var item in properties)
            {
                var attr= item.GetCustomAttributes(typeof(TAttribute),false).FirstOrDefault();
                if (attr!=null)
                {
                    fields.Add(item.Name);
                }
            }
            return fields;*/
        }

        /// <summary>
        /// 获取所有字段
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <returns></returns>
        public static IEnumerable<string> GetFieldsByAttribute<TTableModel>()
        {
            return typeof(TTableModel).GetProperties().Select(a => a.Name);
        }

        /// <summary>
        /// 获取class的特性值 基类有也可以
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TAttribute">一个特性只有一个Name</typeparam>
        /// <returns></returns>
        public static string GetValueByAttribute<TModel, TAttribute>() where TAttribute : Attribute
        {
            //寻找基类的特性
            IList<CustomAttributeTypedArgument> GetArgs(Type type)
            {
                var arguments = type.CustomAttributes.Where(a => a.AttributeType == typeof(TAttribute))
                  .FirstOrDefault()?.ConstructorArguments;//构造 函数参数集合
                if (arguments != null)
                {
                    return arguments;
                }
                else
                {
                    if (type.BaseType != null)
                    {
                        return GetArgs(type.BaseType);
                    }
                    //所有基类都没有找到映射的表名称
                    throw new Exception("None of the base classes found the mapped table name");
                }
            }
            var field= GetArgs(typeof(TModel)).FirstOrDefault();
            return (string)field.Value;
        }

        /// <summary>
        /// 获取枚举的特性值
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetAttrValueByEnum<TModel, TAttribute>(int enumValue) 
            where TModel : Enum
            where TAttribute:Attribute
        {
            var filed = typeof(TModel).GetFields().Where(
                a => a.CustomAttributes.Count() > 0
                && (int)a.GetValue(a.Name) == enumValue
            ).FirstOrDefault();
            if (filed==null)
            {
                //没有找到指定的值或者特性
                return "None";
            }
            var arguments = filed.CustomAttributes.Where(a => a.AttributeType == typeof(TAttribute))
                .FirstOrDefault()?.ConstructorArguments;
            return (string)arguments.FirstOrDefault().Value;
        }
    }
}
