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
        /// ���������
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            InitDirectory();
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// ����web����
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())//�����滻
                .ConfigureLogging((loggingBuilder) =>
                {
                    //Replace the default Logging with Log4Net
                    loggingBuilder.AddLog4(ConfigurationsModel.Log4netConfig);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    /*                    ��Ӧʹ�ö���ͨ����󶨣�http: //*:80/ �� http://+:80���� ����ͨ����󶨻����Ӧ�ð�ȫ©����
                                        ��ʹ����ʽ��������IP ��ַ��������ͨ����� 
                                        ����ɿ�������������������ܹ����� *.com��������ͨ����󶨣����磬*.mysub.com�����ṹ�ɰ�ȫ����
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
        /// ��ʼ��Ŀ¼
        /// </summary>
        private static void InitDirectory()
        {
            var LogDirecotry = AppDomain.CurrentDomain.BaseDirectory + "/wwwroot/Log/";
            new FileInfo(LogDirecotry).Directory.CreateDirectoryInfo();
        }
    }
}
