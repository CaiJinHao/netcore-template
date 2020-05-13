using Common.NetCoreWebUtility.Swagger;
using Common.Utility.Models.Config;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.ApiServices.Extensions.Service
{
    /// <summary>
    /// SwaggerGenServic
    /// </summary>
    public static class SwaggerGenServiceExtensions
    {
        /// <summary>
        /// 添加SwaggerGen组件
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGenService(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                // resolve the IApiVersionDescriptionProvider service
                // note: that we have to build a temporary service provider here because one has not been created yet
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                // add a swagger document for each discovered API version
                // note: you might choose to skip or document deprecated API versions differently
                //添加多个版本需要再配置文件中添加描述
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    var _doc = StaticConfig.AppSettings.ServiceCollectionExtension.SwaggerDoc.Where(a => a.ApiVersion == description.ApiVersion.ToString()).FirstOrDefault();
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = _doc?.Title,
                        Version = description?.ApiVersion.ToString(),
                        Description = _doc?.Description
                    });
                }
                // add a custom operation filter which sets default values
                //options.OperationFilter<SwaggerDefaultValues>();

                var basePath = StaticConfig.ContentRootPath;//Directory.GetCurrentDirectory();
                var xmlPath = Path.Combine(basePath, "Swagger/YourWebApiName.ApiServices.xml");
                options.IncludeXmlComments(xmlPath, true);

                //TODO:暂时没有Models的注释文档
                //var xmlModelPath = Path.Combine(basePath, "Swagger/YourWebApiName.Models.xml");
                //options.IncludeXmlComments(xmlModelPath);


                #region Swagger添加授权验证服务

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                #endregion

                options.SchemaFilter<SwaggerSchemaFilter>();
                //options.ParameterFilter<SwaggerParameterFilter>();
                options.OperationFilter<SwaggerOperationFilter>();
            });
        }
    }
}
