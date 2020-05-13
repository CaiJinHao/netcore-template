using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YourWebApiName.ApiServices.Extensions;
using Common.Utility.Extension;

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
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())//容器替换
                .ConfigureLogging((loggingBuilder) =>
                {
                    //Replace the default Logging with Log4Net
                    loggingBuilder.AddLog4(ConfigurationsModel.Log4netConfig);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    /*                    不应使用顶级通配符绑定（http: //*:80/ 和 http://+:80）。 顶级通配符绑定会带来应用安全漏洞。
                                        请使用显式主机名或IP 地址，而不是通配符。 
                                        如果可控制整个父域（相对于易受攻击的 *.com），子域通配符绑定（例如，*.mysub.com）不会构成安全风险
                                        http://192.168.1.88:9016*/

                    var _path = Directory.GetCurrentDirectory();
                    var config = new ConfigurationBuilder()
                                    .SetBasePath(_path)
                                    .AddJsonFile(ConfigurationsModel.HostSettings, optional: true)
                                    .AddCommandLine(args)
                                    .Build();
                    webBuilder.UseConfiguration(config);
                    webBuilder.ConfigureKestrel(serverOptions => {
                        serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
                        serverOptions.Limits.MaxConcurrentConnections = long.MaxValue;
                    });
                    webBuilder.UseStartup<Startup>();
                });


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
