using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace YourWebApiName.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                             .AddJsonFile("hostsettings.json", optional: true)
                            .AddCommandLine(args)
                            .Build();

            return WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((loggingBuilder) =>
                {
                    //Replace the default Logging with Log4Net
                    loggingBuilder.AddFilter(f => f == LogLevel.Error);
                })
                 .UseConfiguration(config)
                 .UseStartup<Startup>();
        }
    }
}
