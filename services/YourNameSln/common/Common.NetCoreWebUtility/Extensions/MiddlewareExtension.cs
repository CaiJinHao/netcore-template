using Common.NetCoreWebUtility.Middleware;
using Common.Utility.Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.NetCoreWebUtility.Extension
{
    /// <summary>
    /// 中间件引入
    /// </summary>
    public static class MiddlewareExtension
    {
        /// <summary>
        /// 引入所有的自定义中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseNetCoreWebUtilityMiddleware(this IApplicationBuilder app)
        {
            return app
                .UseExceptionHandlerMiddleware()
                .UseHttpRequstRecordMiddleware();
        }

        /// <summary>
        /// 异常处理中间件
        /// 放在最上边能够捕获全局所有异常
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }

        private static IApplicationBuilder UseHttpRequstRecordMiddleware(this IApplicationBuilder app)
        {
            return app
                //.UseMiddleware<HttpRequstRecordMiddleware>()
                ;
        }
    }
}
