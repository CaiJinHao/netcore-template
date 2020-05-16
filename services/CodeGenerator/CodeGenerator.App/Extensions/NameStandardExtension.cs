using CodeGenerator.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.App.Extensions
{
    /// <summary>
    /// 命名规范
    /// </summary>
    public static class NameStandardExtension
    {
        /*
         将由下划线分割的转为驼峰命名
             */
        /// <summary>
        /// 转换为帕斯卡命名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ConvertToPascal(this string name)
        {
            if (StaticConfig.AppSettings.Template.NamingFormat== Models.Enums.EnumNamingFormat.大驼峰Pascal_符号分割)
            {
                return name.Replace("_","");
            }
            StringBuilder result = new StringBuilder();
            // 快速检查
            if (name == null || string.IsNullOrEmpty(name))
            {
                // 没必要转换
                return "";
            }
            else if (!name.Contains("_"))
            {
                // 不含下划线，仅将首字母大写
                return name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
            }
            // 用下划线将原始字符串分割
            string[] camels = name.Split('_');
            foreach (string camel in camels)
            {
                // 跳过原始字符串中开头、结尾的下换线或双重下划线
                if (string.IsNullOrEmpty(camel))
                {
                    continue;
                }
                // 其他的驼峰片段，首字母大写
                result.Append(camel.Substring(0, 1).ToUpper());
                result.Append(camel.Substring(1).ToLower());
            }
            return result.ToString();
        }

        /// <summary>
        /// 转换为驼峰命名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ConvertToCamel(this string name)
        {
            if (StaticConfig.AppSettings.Template.NamingFormat == Models.Enums.EnumNamingFormat.大驼峰Pascal_符号分割)
            {
                var _n = name.Replace("_", "");
                return _n.First().ToString().ToLower() + _n.Substring(1, _n.Length - 1);
            }
            StringBuilder result = new StringBuilder();
            // 快速检查
            if (name == null || string.IsNullOrEmpty(name))
            {
                // 没必要转换
                return "";
            }
            else if (!name.Contains("_"))
            {
                // 不含下划线，仅将首字母大写
                return name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
            }
            // 用下划线将原始字符串分割
            string[] camels = name.Split('_');
            var camels_index = 0;
            foreach (string camel in camels)
            {
                // 跳过原始字符串中开头、结尾的下换线或双重下划线
                if (string.IsNullOrEmpty(camel))
                {
                    continue;
                }
                // 处理真正的驼峰片段
                if (camels_index == 0)
                {
                    // 第一个驼峰片段，全部字母都小写
                    result.Append(camel.ToLower());
                }
                else
                {
                    //其他的驼峰片段，首字母大写
                    result.Append(camel.Substring(0, 1).ToUpper());
                    result.Append(camel.Substring(1).ToLower());
                }
                camels_index++;
            }
            return result.ToString();
        }

        /// <summary>
        /// 转为全小写
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ConvertToLower(this string name)
        {
            return name.ToLower().Replace("_", "");
        }
    }
}
