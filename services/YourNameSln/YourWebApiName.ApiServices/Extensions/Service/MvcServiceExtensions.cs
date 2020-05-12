using Common.NetCoreWebUtility.Filters;
using Common.Utility.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.ApiServices.Extensions.Service
{
    public static class MvcServiceExtensions
    {
        /// <summary>
        /// 添加MVC组件
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMvcService(this IServiceCollection services)
        {
            //DI 使用属性自动注入，需要替换以下规则
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            //此方法将不注册用于视图或页面的服务
            services.AddControllers(options =>
            {
                //全局异常过滤器
                options.Filters.Add(typeof(MvcExceptionsFilter));

                //Model参数验证
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "该字段为必填字段");
                options.MaxModelValidationErrors = 10;//达到该值时验证停止
                options.Filters.Add(typeof(ParametersValidationAttribute));

                //全局授权过滤器，不允许匿名访问
#if !DEBUG
                options.Filters.Add(new AuthorizeFilter("all_access"));
                options.Conventions.Insert(0, new RouteAuthorizeConvention());
#endif

                //路由重写
                //options.Conventions.Insert(0, new RoutePrefixConvention(new RouteAttribute("jinhao")));
            }).SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JsonExtension.jsOptions.Encoder;
                options.JsonSerializerOptions.IgnoreNullValues = JsonExtension.jsOptions.IgnoreNullValues;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = JsonExtension.jsOptions.PropertyNameCaseInsensitive;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonExtension.jsOptions.PropertyNamingPolicy;
                options.JsonSerializerOptions.AllowTrailingCommas = JsonExtension.jsOptions.AllowTrailingCommas;
                options.JsonSerializerOptions.Converters.Add(new Common.Utility.JsonConverter.JhDateTimeConverter());
                /*
                 options.SerializerSettings.ContractResolver = new NullToEmptyStringResolver();//自动将Null自动换位空字符串
                 */
            });

            return services.Configure<ApiBehaviorOptions>(options =>
            {
                //为ture 时禁用
                //禁用请求推理
                options.SuppressConsumesConstraintForFormFileParameters = false;
                //禁用默认推理规则
                options.SuppressInferBindingSourcesForParameters = true;
                //禁用ModelState验证  如果为flase 会复杂类型传不进来
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
