using YourWebApiName.ApiServices.Extensions;
using Common.NetCoreWebUtility.Extension;
using Common.Utility.Models.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourWebApiName.ApiServices.Extensions.Service;

namespace YourWebApiName.ApiServices.Extensions
{
    /// <summary>
    /// ServiceCollectionExtension
    /// </summary>
    public static class ServiceCollectionExtension
    {
        private static IServiceCollection InitAppSettings(this IServiceCollection services)
        {
            var configurationAppConfig = new ConfigurationBuilder()
                          .SetBasePath(StaticConfig.ContentRootPath)
                          .AddJsonFile(ConfigurationsModel.AppSettings, optional: true, reloadOnChange: true)
                          .Build();
            services.Configure<AppSettings>(configurationAppConfig.GetSection("AppSettings"));

            var provider = services.BuildServiceProvider();
            StaticConfig.AppSettings = provider.GetService<IOptions<AppSettings>>().Value;

            return services;
        }


        /// <summary>
        /// 初始化所有服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            return services.InitAppSettings()
                .AddNetCoreWebUtilityServices()
                .AddResponseCompression()//启用响应压缩可以返回更大的数据内容
                .AddCorsService()
                .AddSwaggerGenService()
                .AddMiniProfilerService()
                .AddMvcService()
                .AddWebApiVersionService()
                .AddAuthorizationService()
                .AddAuthenticationService()
                .AddCustomSingletonService()
                //.AddConsulComponentServices(ConfigurationsModel.ConsulSettings)//TODO:添加consul服务
                .AddOtherService()
                .AddBackgroundTasks();
        }

        /// <summary>
        /// 添加其他组件
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddOtherService(this IServiceCollection services)
        {
            return services.AddSession()//使用Session
                           .AddMemoryCache();//使用本地缓存必须添加
        }

        /// <summary>
        /// 添加后台任务执行服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddBackgroundTasks(this IServiceCollection services)
        {
            //var backgroundTasksConfig = appSettings.ServiceCollectionExtension.BackgroundTasks;
            //if (backgroundTasksConfig.EnabledDeviceDetectionService)
            //{
            //    services.AddHostedService<DataDeleteService>();
            //}
            return services;
        }
    }
}
