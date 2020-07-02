using Autofac;
using Autofac.Extras.DynamicProxy;
using Common.Utility.AOP;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Utility.Autofac
{
    public static class DependencyInjectionModule
    {
        /// <summary>
        /// 未实现接口的，如APP
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblys"></param>
        public static void RegisterDefaultModule(this ContainerBuilder builder, Type[] aopServices, params Assembly[] assemblys)
        {
            foreach (var assembly in assemblys)
            {
                builder.RegisterAssemblyTypes(assembly)
                       .PropertiesAutowired()
                       .InstancePerLifetimeScope()
                       .InterceptedBy(aopServices);
                //.EnableClassInterceptors()
                ;
            }
        }

        /// <summary>
        /// 实现接口的才能用该方法注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblys"></param>
        public static void RegisterDefaultModuleImplementedInterfaces(this ContainerBuilder builder, Type[] aopServices, params Assembly[] assemblys)
        {
            /*
             InstancePerLifetimeScope：同一个Lifetime生成的对象是同一个实例
            SingleInstance：单例模式，每次调用，都会使用同一个实例化的对象；每次都用同一个对象；
            InstancePerDependency：默认模式，每次调用，都会重新实例化对象；每次请求都创建一个新的对象；
             */
            //var cacheType = new List<Type>();
            //cacheType.Add(typeof(BlogCacheAOP));
            foreach (var assembly in assemblys)
            {
                builder.RegisterAssemblyTypes(assembly)//注册类型
                       .AsImplementedInterfaces()//实现接口
                       .PropertiesAutowired()//允许属性自动注入
                       //.InstancePerLifetimeScope()//生命周期管理器
                       .InstancePerDependency()
                                                                        //.EnableClassInterceptors()//开启类拦截器，只有类才能开启
                       .EnableInterfaceInterceptors()//开启接口拦截器，只有接口才能开启
                       .InterceptedBy(aopServices);//允许将拦截器服务的列表分配给注册。 
                ;
            }
        }

        /// <summary>
        /// 注册AOP切面
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterAop(this ContainerBuilder builder)
        {
            builder.RegisterType<LogAOP>();
        }
    }
}
