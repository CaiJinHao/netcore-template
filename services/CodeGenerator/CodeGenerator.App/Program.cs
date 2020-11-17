using CodeGenerator.App.Models;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.App.Extensions;
using CodeGenerator.App.DbModels;
using System.Configuration;
using CodeGenerator.Services;
using CodeGenerator.App.BuildFiles;

namespace CodeGenerator.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await RegisterConfig.InitConfig();

            start_build_code:
            Console.WriteLine("是否要指定表生成，请选择选项：");
            Console.WriteLine("0:生成所有表,1:指定表生成");
            var is_table = Console.ReadLine();
            var tableStr = string.Empty;
            if (is_table.Equals("1"))
            {
                startInPutTable:
                Console.WriteLine("请输入要构建的表名称以“,”隔开：");
                tableStr = Console.ReadLine();
                try
                {
                    tableStr.Split(',');
                }
                catch (Exception)
                {
                    Console.WriteLine("格式不正确");
                    goto startInPutTable;
                }
            }

            Console.WriteLine("是否要指定模板生成(FileDirName)：");
            Console.WriteLine("0:生成所有模板,1:指定模板生成");
            var is_template = Console.ReadLine();
            var templateStr = string.Empty;
            if (is_template.Equals("1"))
            {
            startInPutTable:
                Console.WriteLine("请输入要构建的模板文件夹名称(FileDirName)以“,”隔开：");
                templateStr = Console.ReadLine();
                try
                {
                    templateStr.Split(',');
                }
                catch (Exception)
                {
                    Console.WriteLine("格式不正确");
                    goto startInPutTable;
                }
            }

            var path= await new BuildModels().BuildStart(tableStr, templateStr);
            Console.WriteLine("输出文件路径："+path);
            Console.WriteLine("是否从新开始：0:1");
            if (Console.ReadLine().Equals("1"))
            {
                goto start_build_code;
            }
        }
    }
}
