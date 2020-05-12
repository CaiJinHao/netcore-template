using Autofac.Extensions.DependencyInjection;
using Common.NetCoreWebUtility.Extension;
using Common.Utility.Autofac;
using Common.Utility.Models.Config;
using Common.Utility.Models.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.ApiServices.Extensions
{
    public static class MiddlewareExtension
    {
        /// <summary>
        /// 引入所有组件中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder app)
        {
            // ↓↓↓↓↓↓ 注意下边这些中间件的顺序，很重要 ↓↓↓↓↓↓
            AutofacHelper.Container = app.ApplicationServices.GetAutofacRoot();//只能在Configure中赋值
            return app
                .UseNetCoreWebUtilityMiddleware()
                .UseAppMiddleware()
                .StartHostedService()
                ;
        }

        /// <summary>
        /// 引入所有APP中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private static IApplicationBuilder UseAppMiddleware(this IApplicationBuilder app)
        {
            // ↓↓↓↓↓↓ 注意下边这些中间件的顺序，很重要 ↓↓↓↓↓↓

            return app
                .UseResponseCompression()//启用响应压缩可以返回更大的数据内容,必须在app.UseMvc 之前调用。
                .UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                })
                .UseCors("LimitRequests")
                .UseHsts()//强制客户端（如浏览器）使用HTTPS与服务器创建连接
                          //.UseHttpsRedirection() //跳转https
                .UseStaticHttpContextMiddleware()//全局HTTP
                .UseStaticFilesMiddleware()//使用静态文件
                .UseCookiePolicy()//使用cookie
                .UseStatusCodePages()//把错误码返回前台，比如是404
                .UseRouting()//Routing
                .UseAuthentication()//开启认证
                .UseAuthorization()//开启授权策略
                .UseMiniProfiler() //http 请求分析

                //.UseConsulComponentMiddleware()//TODO:使用consul组件服务注册
                ;
        }

        /// <summary>
        /// 全局HTTP中间件 线程安全
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private static IApplicationBuilder UseStaticHttpContextMiddleware(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            UserHttpContext.Configure(httpContextAccessor);
            return app;
        }

        /// <summary>
        /// 引入静态文件中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private static IApplicationBuilder UseStaticFilesMiddleware(this IApplicationBuilder app)
        {
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("login.html");
            return app
                .UseDefaultFiles(options)//默认文档
                .UseStaticFiles(new StaticFileOptions
                {
                    //如果文件不是可识别的类型是否应该提供其他服务端
                    ServeUnknownFileTypes = true,
                    OnPrepareResponse = ctx =>
                    {
                        if (!"application/json".Equals(ctx.Context.Response.ContentType))//json文件不缓存
                        {
                            ctx.Context.Response.Headers.Add("Cache-Control", $"public, max-age={StaticConfig.AppSettings.MiddlewareExtension.StaticFileCachePeriod}");
                        }
                    }
                });
        }

        /// <summary>
        /// 启动后台托管服务
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private static IApplicationBuilder StartHostedService(this IApplicationBuilder app)
        {
            //var backgroundTasksConfig = StaticConfig.AppSettings.ServiceCollectionExtension.BackgroundTasks;
            //if (backgroundTasksConfig.EnabledDeviceDetectionService)
            //{
            //    new Task(async () => {
            //        //var sysStorehouseService = AutofacHelper.GetService<ISysStorehouseService>();
            //    }).Start();
            //}
            return app;
        }
    }
}
