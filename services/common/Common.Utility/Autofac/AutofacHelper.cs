using Autofac;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Autofac
{
    /// <summary>
    /// Autofac Ioc
    /// </summary>
    public class AutofacHelper
    {
        /// <summary>
        /// 需要再Configure中添加 AutofacHelper.Container = app.ApplicationServices.GetAutofacRoot();
        /// 在 UseAllMiddleware 中赋值,其他地方赋值不了
        /// </summary>
        public static ILifetimeScope Container { get; set; }

        /// <summary>
        /// 获取服务(Single)
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T GetService<T>() where T : class
        {
            return Container.Resolve<T>();
        }

        /*
        请使用GetService
        /// <summary>
        /// 获取服务(请求生命周期内)
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T GetScopeService<T>() where T : class
        {
            return (T)GetService<IHttpContextAccessor>().HttpContext.RequestServices.GetService(typeof(T));
        }*/
    }
}
