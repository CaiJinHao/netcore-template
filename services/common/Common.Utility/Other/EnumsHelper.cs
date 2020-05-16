﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Common.Utility.Other
{
    public class EnumsHelper
    {
        /// <summary>
        /// 根据枚举获取下拉列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<dynamic> GetEnumList<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            foreach (var item in values)
            {
                yield return new
                {
                    Text = item.ToString(),
                    Value = (int)item
                };
            }
        }

        /// <summary>
        /// 返回描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<dynamic> GetEnumListByDescription<T>() where T : Enum
        {
            var fileds = typeof(T).GetFields().Where(a => a.FieldType == typeof(T));
            foreach (var _filed in fileds)
            {
                var _v = _filed.GetValue(_filed.Name);
                if (_filed.CustomAttributes.Count() > 0)
                {
                    var arguments = _filed.CustomAttributes
                    .Where(a => a.AttributeType == typeof(DescriptionAttribute)).FirstOrDefault()?.ConstructorArguments;
                    yield return new
                    {
                        Text = arguments.First().Value,
                        Value = (int)_v
                    };
                }
                //else
                //{
                //    yield return new
                //    {
                //        Text = _filed.Name,
                //        Value = (int)_v
                //    };
                //}
            }
        }
    }
}
