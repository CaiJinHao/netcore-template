using CodeGenerator.App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.App.Extensions
{
    public static  class FilesExtension
    {
        /// <summary>
        /// 读取String文件内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<string> ReadFileAsync(this string path)
        {
            path = Path.Combine(StaticConfig.ContentRootPath, path);
            using (var sr = new StreamReader(path, Encoding.Default))
            {
                return await sr.ReadToEndAsync();
            }
        }

        /// <summary>
        /// 将字符串保存到文件
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SaveFile(this string fileFullName, string content)
        {
            string dir = Path.GetDirectoryName(fileFullName);
            if (!Directory.Exists(dir))
            { 
                Directory.CreateDirectory(dir);
            }
            File.WriteAllText(fileFullName, content);
            return fileFullName;
        }
    }
}
