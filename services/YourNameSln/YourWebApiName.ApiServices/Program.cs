using Autofac.Extensions.DependencyInjection;
using Common.Utility.Extension;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using YourWebApiName.ApiServices.Extensions;

namespace YourWebApiName.ApiServices
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 主程序入口
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            InitDirectory();
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 创建web主机
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateHostBuilder(string[] args) 
        {
            var _path = Directory.GetCurrentDirectory();
            var config = new ConfigurationBuilder()
                            .SetBasePath(_path)
                            .AddJsonFile(ConfigurationsModel.HostSettings, optional: true)
                            .AddCommandLine(args)
                            .Build();

            return WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((loggingBuilder) =>
                {
                    //Replace the default Logging with Log4Net
                    loggingBuilder.AddLog4(ConfigurationsModel.Log4netConfig);
                })
                 .UseConfiguration(config)
                 //.ConfigureKestrel(serverOptions =>
                 //{
                 //    serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
                 //    serverOptions.Limits.MaxConcurrentConnections = long.MaxValue;
                 //})
                 .UseStartup<Startup>();
        }


        /// <summary>
        /// 初始化目录
        /// </summary>
        private static void InitDirectory()
        {
            var LogDirecotry = AppDomain.CurrentDomain.BaseDirectory + "/wwwroot/Log/";
            new FileInfo(LogDirecotry).Directory.CreateDirectoryInfo();
        }
    }
}
