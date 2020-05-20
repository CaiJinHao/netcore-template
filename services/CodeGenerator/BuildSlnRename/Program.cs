using BuildSlnRename.DirectoryServices;
using BuildSlnRename.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BuildSlnRename.Models;

namespace BuildSlnRename
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*
             1.修改文件内容的名称
                遍历所有文件，使用正则表达式查找匹配的内容做替换（YourWebApiName替换为{你的项目名称}）
                修改包括：YourWebApiName命名空间，
             2.修改文件名称
                修改包括：.csproj，
             2.修改文件夹名称
                修改包括：
                YourWebApiName.ApiServices，YourWebApiName.IRepository，YourWebApiName.IServices
                YourWebApiName.Repository，YourWebApiName.Services，YourWebApiName.Models
                可选修改（不修改就可以直接删掉，建议修改要不然很多都可以删掉）：
             */

            var  AppSettings = await "Configurations/appsettings.json".ReadJson<AppSettings>();

            var sourceDir = @"E:\MyWork\test\webapicommon";
            var dirOperator = new DirectoryOperator(
                new Regex("git"),
                new Regex("git|documents|AutoUpdateServer|common|database"),
               AppSettings.FileReplaceModels,
               AppSettings.ContentReplaceModels);
            dirOperator.DirectoryHandler(sourceDir);
            Console.WriteLine("Over");
            Console.ReadKey();
        }
    }
}
