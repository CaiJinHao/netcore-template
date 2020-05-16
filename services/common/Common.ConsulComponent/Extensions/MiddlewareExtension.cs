using Common.ConsulComponent.Builder;
using Common.ConsulComponent.Models;
using Common.Utility.Other;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Common.ConsulComponent.Extensions
{
    /// <summary>
    /// 中间件引入
    /// </summary>
    public static class MiddlewareExtension
    {
        /// <summary>
        /// 引入所有组件中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseConsulComponentMiddleware(this IApplicationBuilder app)
        {
            return app
                .UseHealthChecks(StaticConsulConfig.ConsulSettings.HealthCheck)
                .UseConsulMiddleware();
        }


        /// <summary>
        /// Consul 中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseConsulMiddleware(this IApplicationBuilder app)
        {
            // 获取主机生命周期管理接口
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            // 获取服务配置项
            var serviceOptions = app.ApplicationServices.GetRequiredService<IOptions<ConsulSettingsOptions>>().Value;

            // 服务ID必须保证唯一
            //serviceOptions.ServiceId = Guid.NewGuid().ToString();

            var consulClient = new ConsulClient(configuration =>
            {
                //服务注册的地址，集群中任意一个地址
                configuration.Address = new Uri(serviceOptions.ConsulAddress);
            });

            /*
            // 获取当前服务地址和端口 暂时不用因为linux可能获取成127.0.0.1导致不能访问
            var features = app.Properties["server.Features"] as FeatureCollection;
            var addressCollection = features?.Get<IServerAddressesFeature>().Addresses;
            var address = addressCollection.First().Replace("*", IpHelper.GetLocalIP()); 
            var uri = new Uri(address);
            */

            var uri = new Uri(serviceOptions.LocalServerAddress);

            // 节点服务注册对象
            var registration = new AgentServiceRegistration()
            {
                ID = serviceOptions.ServiceId,
                Name = serviceOptions.ServiceName,// 服务名 由于中文名会乱码，用id然后去数据库查
                Address = uri.Host,
                Port = uri.Port, // 服务端口
                Check = new AgentServiceCheck
                {
                    // 注册超时
                    Timeout = TimeSpan.FromSeconds(5),
                    // 服务停止多久后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 健康检查地址
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}{serviceOptions.HealthCheck}",
                    // 健康检查时间间隔
                    Interval = TimeSpan.FromSeconds(10),
                }
            };

            // 注册服务
            consulClient.Agent.ServiceRegister(registration).Wait();

            // 应用程序终止时，注销服务
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceOptions.ServiceId).Wait();
            });

            return app;
        }
    }
}
