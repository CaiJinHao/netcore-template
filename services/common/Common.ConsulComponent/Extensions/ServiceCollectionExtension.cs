using Common.ConsulComponent.Builder;
using Common.ConsulComponent.Models;
using Common.Utility.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ConsulComponent.Extensions
{
    /// <summary>
    /// 初始化服务集合
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 初始化所有服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsulComponentServices(this IServiceCollection services
            , string ConsulSettingsJsonFile)
        {
            services.AddHealthChecks();
            return services.RegisterConsulConfig(ConsulSettingsJsonFile);
        }

        /// <summary>
        /// 注册consul配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterConsulConfig(this IServiceCollection services, string ConsulSettingsJsonFile)
        {
            // 读取服务配置文件
            var config = new ConfigurationBuilder()
                .SetBasePath(StaticConfig.ContentRootPath)
                .AddJsonFile(ConsulSettingsJsonFile).Build();
            services.Configure<ConsulSettingsOptions>(config);

            //TODO:consul服务注册
            var provider = services.BuildServiceProvider();
            StaticConsulConfig.ConsulSettings = provider.GetService<IOptions<ConsulSettingsOptions>>().Value;

            return services;
        }
    }
}
