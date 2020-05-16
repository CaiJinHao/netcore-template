using Common.Utility.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.NetCoreWebUtility.Extension
{
    /// <summary>
    /// 初始化服务集合
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 初始化NetCoreUtility 中所有自定义服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddNetCoreWebUtilityServices(this IServiceCollection services)
        {
            return services.AddMemoryCacheComponent();
        }

        private static IServiceCollection AddMemoryCacheComponent(this IServiceCollection services)
        {
            return  services
                            .AddScoped<ICaching, MemoryCaching>()//HTTP请求有效
                            .AddSingleton<IMemoryCache>(factory => //应用程序有效
                            {
                                var cache = new MemoryCache(new MemoryCacheOptions());
                                return cache;
                            });
        }
    }
}
