using YourWebApiName.IdentityServer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.IdentityServer.Extensions
{
    public static class RegisterConfig
    {
        /// <summary>
        /// 初始化系统配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        public static IServiceCollection InitConfig(this IServiceCollection services, Func<IServiceCollection, IServiceCollection> action)
        {
            services.InitAppSettings();
            return action(services);
        }

        /// <summary>
        /// 初始化appsettins.json AppSettings 对象
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection InitAppSettings(this IServiceCollection services)
        {
            var configurationAppConfig = new ConfigurationBuilder()
                           .SetBasePath(StaticConfigModel.ContentRootPath)
                           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                           .Build();
            return services.Configure<AppSettingsModel>(configurationAppConfig.GetSection("AppSettings"));
        }
    }
}
