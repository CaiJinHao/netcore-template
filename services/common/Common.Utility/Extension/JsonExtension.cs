using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Linq;

namespace Common.Utility.Extension
{
    /// <summary>
    /// JSON 扩展
    /// </summary>
    public static class JsonExtension 
    {
        public static readonly JsonSerializerOptions jsOptions = new JsonSerializerOptions
        {
            //解决中文符号中文字不能正确序列化问题
            //Solve the problem that Chinese characters cannot be serialized correctly
            //Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            //取消Unicode编码
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All),
            IgnoreNullValues = false,//忽略NULL
            PropertyNameCaseInsensitive=true,//区分大小写
            PropertyNamingPolicy=null,//使用默认属性名
            AllowTrailingCommas=true,//允许额外符号
        };

        /// <summary>
        /// 序列化对象
        /// Serialized object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Serialize(this object value)
        {
            return JsonSerializer.Serialize(value: value, options: jsOptions);
        }

        /// <summary>
        /// 转义
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Escape(this string value)
        {
            return System.Text.RegularExpressions.Regex.Replace(value,"\"","\\\"");
        }

        /// <summary>
        /// 反序列化对象
        /// Deserialized object
        /// </summary>
        /// <typeparam name="TValue">Object type</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TValue Deserialize<TValue>(this string value)
        {
            return JsonSerializer.Deserialize<TValue>(json: value, options: jsOptions);
        }
    }
}
