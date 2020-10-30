using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.ApiServices.Extensions.Service
{
    /// <summary>
    /// MiniProfilerServiceExtensions
    /// </summary>
    public static class MiniProfilerServiceExtensions
    {
        /// <summary>
        /// AddMiniProfilerService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMiniProfilerService(this IServiceCollection services)
        {
            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";
                options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.Left;
                options.PopupShowTimeWithChildren = true;

                // 可以增加权限
                //options.ResultsAuthorize = request => request.HttpContext.User.Claims.Where(a=>a.Type=="rolename").First().Value== "jinhao";
                //options.UserIdProvider = request => request.HttpContext.User.Identity.Name;


                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();

                options.IgnoredPaths.Add("/wwwroot");
            });
            return services;
        }
    }
}
