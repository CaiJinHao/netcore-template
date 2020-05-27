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
            FilesHandler(sourceDir);
            var dirs = Directory.EnumerateDirectories(sourceDir).Select(r=>new DirectoryInfo(r))
                .Where(rinfo => ignoreDirectoriesRegex.IsMatch(rinfo.Name) == false);//拿出不匹配的目录
            foreach (var _dir in dirs)
            {
                FilesHandler(_dir.FullName);
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
        public  void FilesHandler(string _dirFullName)
        {
            var files = Directory.GetFiles(_dirFullName).Select(f => new FileInfo(f))
                    .Where(f => ignoreFileRegex.IsMatch(f.Name) == false).ToArray();
            if (files.Length > 0)
            {
                foreach (var _file in files)
                {
                    ReplaceFilesContent(_file);
                    {//修改文件名称
                        foreach (var reg in fileReplaces)
                        {
                            if (reg.SearchRegex.IsMatch(_file.Name))
                            {//正则匹配成功，开始修改文件名
                                var directoryName = Path.Combine(_file.DirectoryName, reg.SearchRegex.Replace(_file.Name, reg.NewContent));
                                _file.MoveTo(directoryName);
                                Console.WriteLine($"修改过文件名称：{_file.FullName}:{directoryName}");
                            }
                        }
                    }
                }
            }
        }

        public void ReplaceFilesContent(FileInfo file)
        {
            var b = false;
            var text = string.Empty;
            using (StreamReader sr = file.OpenText())
            {
                text = sr.ReadToEnd();
              
                foreach (var reg in contentReplaces)
                {
                    if (reg.SearchRegex.IsMatch(text))
                    {//当内容匹配的时候，替换内容
                        text = reg.SearchRegex.Replace(text, reg.NewContent);
                        b = true;
                    }
                }
            }
            if (b)
            {
                using (StreamWriter sw = file.CreateText())
                {
                    sw.WriteLine(text);
                    Console.WriteLine($"修改了文件内容：{file.FullName}");
                }
            }

        }
    }
}
