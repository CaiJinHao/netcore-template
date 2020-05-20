using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BuildSlnRename.DirectoryServices
{
    public  class DirectoryOperator
    {
        /// <summary>
        /// 忽略的文件
        /// </summary>
        private Regex ignoreFileRegex { get; set; }
        /// <summary>
        /// 忽略的目录
        /// </summary>
        private Regex ignoreDirectoriesRegex { get; set; }
        /// <summary>
        /// 目录名文件名替换
        /// </summary>
        private ReplaceModel[] fileReplaces { get; set; }
        /// <summary>
        /// 文件内容替换
        /// </summary>
        private ReplaceModel[] contentReplaces { get; set; }
        public DirectoryOperator(Regex _ignoreFileRegex, Regex _ignoreDirectoriesRegex
            , ReplaceModel[] _fileReplaces, ReplaceModel[] _contentReplaces)
        {
            ignoreFileRegex = _ignoreFileRegex;
            ignoreDirectoriesRegex = _ignoreDirectoriesRegex;
            fileReplaces = _fileReplaces;
            contentReplaces = _contentReplaces;
        }

        public  void DirectoryHandler(string sourceDir)
        {
            var dirs = Directory.EnumerateDirectories(sourceDir).Select(r=>new DirectoryInfo(r))
                .Where(rinfo => ignoreDirectoriesRegex.IsMatch(rinfo.Name) == false);//拿出不匹配的目录
            foreach (var _dir in dirs)
            {
                Console.WriteLine(_dir);
                var files = Directory.GetFiles(_dir.FullName).Select(f => new FileInfo(f))
                    .Where(f => ignoreFileRegex.IsMatch(f.Name) == false).ToArray();
                if (files.Length > 0)
                {
                    FilesHandler(files);
                }
                //获取目录
                DirectoryHandler(_dir.FullName);
                {//修改目录名称
                    foreach (var reg in fileReplaces)
                    {
                        if (reg.SearchRegex.IsMatch(_dir.Name))
                        {//正则匹配成功，开始修改文件名
                            var directoryName = Path.Combine(_dir.Parent.FullName, reg.SearchRegex.Replace(_dir.Name, reg.NewContent));
                            _dir.MoveTo(directoryName);
                            Console.WriteLine($"修改文件夹名称：{_dir.FullName}:{directoryName}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 文件处理
        /// </summary>
        /// <param name="files"></param>
        public  void FilesHandler(FileInfo[] files)
        {
            foreach (var _file in files)
            {
                Console.WriteLine(_file.FullName);
                ReplaceFilesContent(_file);
                {//修改文件名称
                    foreach (var reg in fileReplaces)
                    {
                        if (reg.SearchRegex.IsMatch(_file.Name))
                        {//正则匹配成功，开始修改文件名
                            var directoryName = Path.Combine(_file.DirectoryName,reg.SearchRegex.Replace(_file.Name, reg.NewContent));
                            _file.MoveTo(directoryName);
                            Console.WriteLine($"修改过文件名称：{_file.FullName}:{directoryName}");
                        }
                    }
                }
            }
        }

        public void ReplaceFilesContent(FileInfo file)
        {
            var b = false;
            var text = string.Empty;
            // Open the file to read from.
            using (StreamReader sr = file.OpenText())
            {
                text = sr.ReadToEnd();
                foreach (var reg in contentReplaces)
                {
                    if (reg.SearchRegex.IsMatch(text))
                    {//当内容匹配的时候，替换内容
                        reg.SearchRegex.Replace(text, reg.NewContent);
                        b = true;
                        Console.WriteLine($"修改了文件内容：{file.FullName}");
                    }
                }
            }
            if (b)
            {
                //替换完成 保存
                using (FileStream fs = file.OpenWrite())
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(text);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }
        }
    }
}
