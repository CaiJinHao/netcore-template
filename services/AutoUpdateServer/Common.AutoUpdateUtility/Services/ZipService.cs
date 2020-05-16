using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.AutoUpdateUtility.Services
{
    /// <summary>
    /// ZIP服务
    /// </summary>
    public class ZipService
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="zipFileName">zip文件路径</param>
        /// <param name="fromDirectory">要压缩的文件夹</param>
        public static void Compress(string zipFile,string fromDirectory,int compressLevel)
        {
            //获取文件夹下的所有目录
            var files = new DirectoryInfo(fromDirectory).GetFiles();
            using (ZipOutputStream zos = new ZipOutputStream(File.Create(zipFile)))
            {
                zos.SetLevel(compressLevel);
                foreach (var item in files)
                {
                    var zipEntry = new ZipEntry(item.Name);
                }
            }
        }

        /// <summary>
        /// zip解压
        /// </summary>
        /// <param name="sourceFile"></param>
        public static void DeCompress(string zipFile,string toDirectory)
        {
            if (!File.Exists(zipFile))
            {
                throw new ArgumentException("要解压的文件不存在");
            }
            //var path = Directory.GetParent(sourceFile).FullName;
            //var root = Path.GetDirectoryName(zipFile);
            //var fileName = Path.GetFileNameWithoutExtension(zipFile);
            using (var inputStream=new ZipInputStream(File.OpenRead(zipFile)))
            {
                while (true)
                {
                    var zipNextEntry = inputStream.GetNextEntry();
                    if (zipNextEntry == null)
                    {
                        break;
                    }
                    if (!Directory.Exists(toDirectory))
                    {
                        Directory.CreateDirectory(toDirectory);//创建解压到的文件夹
                    }
                    var file = Path.Combine(toDirectory, zipNextEntry.Name);
                    using (var streamWriter = File.Create(file))
                    {
                        inputStream.CopyTo(streamWriter);
                    }
                }
            }
        }
    }
}
