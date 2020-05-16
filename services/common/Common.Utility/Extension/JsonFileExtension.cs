using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Utility.Extension
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

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileFullName"></param>
        /// <param name="_data"></param>
        public static void SaveJson<T>(this string fileFullName,T _data)
        {
            string dir = Path.GetDirectoryName(fileFullName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllText(fileFullName,_data.Serialize());
        }

    }
}
