using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Reflection;

namespace YourWebApiName.ApiServices.Extensions.Middleware
{
    /// <summary>
    /// SwaggerMiddlewareExtensions
    /// </summary>
    public static class SwaggerMiddlewareExtensions
    {
        /// <summary>
        /// Swagger组件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            return app.UseSwagger(c => {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                });
            }).UseSwaggerUI(ui =>
            {
                // build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions.OrderByDescending(a => a.ApiVersion))
                {
                    ui.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }

                //将swagger首页，设置成我们自定义的页面，记得这个字符串的写法：解决方案名.index.html
                //这里是配合MiniProfiler进行性能监控的YourWebApiName.ApiServices namespace
                //index.html页面修改需要重新生成项目
                ui.IndexStream = () => typeof(Startup).GetTypeInfo().Assembly
                .GetManifestResourceStream("YourWebApiName.ApiServices.Swagger.index.html");
                //路由地址
                ui.RoutePrefix = "restapi";

                // Display
                ui.DefaultModelExpandDepth(2);
                ui.DefaultModelRendering(ModelRendering.Model);
                ui.DefaultModelsExpandDepth(-1);
                ui.DisplayOperationId();
                ui.DisplayRequestDuration();
                ui.DocExpansion(DocExpansion.None);
                ui.EnableDeepLinking();
                ui.EnableFilter();
                ui.ShowExtensions();

                // Network
                ui.EnableValidator();
                //ui.SupportedSubmitMethods();

                // Other
                ui.DocumentTitle = "云盒 RESTfull Api";
                ui.InjectStylesheet("/ext/custom-stylesheet.css");
                ui.InjectJavascript("/ext/custom-javascript.js");
            });
        }
    }
}
