using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace YourWebApiName.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var _path = Directory.GetCurrentDirectory();
                    var config = new ConfigurationBuilder()
                                    .SetBasePath(_path)
                                    .AddJsonFile("hostsettings.json", optional: true)
                                    .AddCommandLine(args)
                                    .Build();
                    webBuilder.UseConfiguration(config);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
