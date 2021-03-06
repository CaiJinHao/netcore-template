﻿using Common.NetCoreWebUtility.Filters;
using Common.Utility.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.ApiServices.Extensions.Service
{
    /// <summary>
    /// MVC服务
    /// </summary>
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
            services.AddMvc(options =>
            {
                //全局异常过滤器,谁最后添加先执行谁
                options.Filters.Add(typeof(MvcExceptionsFilter));

                //Model参数验证
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "该字段为必填字段");
                options.MaxModelValidationErrors = 10;//达到该值时验证停止
                options.Filters.Add(typeof(ParametersValidationAttribute));

                //全局授权过滤器，不允许匿名访问，只能通过Token进行授权访问
#if !DEBUG
                options.Filters.Add(new AuthorizeFilter("all_access"));
                options.Conventions.Insert(0, new RouteAuthorizeConvention());
#endif

                //路由重写
                //options.Conventions.Insert(0, new RoutePrefixConvention(new RouteAttribute("jinhao")));
            }).SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddJsonOptions(options =>
            {
                //使用model中的属性 名称返回
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();//使用字段原名称
                //options.SerializerSettings.ContractResolver = new NullToEmptyStringResolver();//自动将Null自动换位空字符串
                //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //不包含属性null的序列化
                //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
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
