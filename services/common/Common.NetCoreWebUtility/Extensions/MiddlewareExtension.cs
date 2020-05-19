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
                .UseHttpRequstRecordMiddleware();
        }

        /// <summary>
        /// 记录HTTP请求
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private static IApplicationBuilder UseHttpRequstRecordMiddleware(this IApplicationBuilder app)
        {
            return app
                //.UseMiddleware<HttpRequstRecordMiddleware>()
                ;
        }
    }
}
