using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.ApiServices.Extensions.Service
{
    /// <summary>
    /// WebApiVersionServiceExtensions
    /// </summary>
    public static class WebApiVersionServiceExtensions
    {
        /// <summary>
        /// 添加版本控制组件
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebApiVersionService(this IServiceCollection services)
        {
            return services.AddVersionedApiExplorer(
                            options =>
                            {
                                options.GroupNameFormat = "'v'VVV";

                                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                                // can also be used to control the format of the API version in route templates
                                options.SubstituteApiVersionInUrl = true;
                            })
                            .AddApiVersioning(config =>
                            {
                                config.ReportApiVersions = true;
                                //是否在没有填写版本号的情况下使用默认版本
                                config.AssumeDefaultVersionWhenUnspecified = true;
                                config.DefaultApiVersion = new ApiVersion(1, 0);
                            });
        }
    }
}
