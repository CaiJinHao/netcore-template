using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodeGenerator.App.Extensions
{
    public static class JsonFileExtension
    {
        /// <summary>
        /// 读取Json文件到对象实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public static async Task<T> ReadJson<T>(this String fileFullName)
        {
            using (var fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read))
            {
                return await JsonSerializer.DeserializeAsync<T>(fs);
            }
        }
    }
}
