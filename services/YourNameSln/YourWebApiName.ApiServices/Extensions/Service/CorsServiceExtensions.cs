using Common.Utility.Models.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.ApiServices.Extensions.Service
{
    /// <summary>
    /// 跨域设置
    /// </summary>
    public static class CorsServiceExtensions
    {
        /// <summary>
        /// 添加跨域策略
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsService(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                /*注意端口号后不要带/斜杆
                注意，http://127.0.0.1:1818 和 http://localhost:1818 是不一样的，尽量写两个*/
                foreach (var item in StaticConfig.AppSettings.ServiceCollectionExtension.Cors.Policy)
                {
                    options.AddPolicy(item.Name,
                                       policy =>
                                       {
                                           policy
                                                .AllowAnyOrigin()
                                                //.WithOrigins(item.Origins)//指定域名进行跨域
                                                .AllowAnyHeader()
                                                .AllowAnyMethod()
                                                ;
                                       });
                }
            });
        }
    }
}
