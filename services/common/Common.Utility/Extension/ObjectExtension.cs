using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Common.Utility.Other;
using System.Reflection;
using System.Text.Json;

namespace Common.Utility.Extension
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 将对象中的值赋值给自己
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">自己</param>
        /// <param name="tobj">对象</param>
        public static void CloneTo<T>(this object sourceObj, T toObj)
        {
            if (sourceObj == null)
            {
                return;
            }
            var sourceInfo = sourceObj.GetType().GetProperties();
            var toInfo = toObj.GetType().GetProperties();
            foreach (var toItem in toInfo)
            {
                var toFiled = sourceInfo.Where(a => a.Name == toItem.Name).FirstOrDefault();
                if (toFiled != null)
                {
                    var v = toItem.GetValue(sourceObj, null);
                    if (v != null && toItem.CanWrite)
                    {
                        toFiled.SetValue(toObj, v, null);
                    }
                }
            }
        }
        
        /// <summary>
        /// 判断对象是否为null
        /// </summary>
        /// <param name="sourceObj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object sourceObj)
        {
            if (sourceObj==null)
            {
                return false;
            }
            var _type = sourceObj.GetType();
            switch (_type.Name)
            {
                case "String":
                    {
                        var _value = (string)sourceObj;
                        return !string.IsNullOrEmpty(_value);
                    }
                case "Int64":
                    {
                        return (Int64)sourceObj > 0;
                    }
                case "DateTime":
                    {
                        var _v = (DateTime)sourceObj;
                        return _v > new DateTime(1900, 1, 1);
                    }
                case "String[]":
                    {
                        var _v = sourceObj as String[];
                        return _v.Length > 0;
                    }
                default:
                    break;
            }
            return false;
        }
    }
}
