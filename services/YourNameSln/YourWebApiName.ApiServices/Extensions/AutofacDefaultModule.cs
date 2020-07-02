//#define TEST

using Autofac;
using Common.Utility.AOP;
using System;
using System.Collections.Generic;
using Common.Utility.Autofac;
using System.Reflection;
using System.IO;
using Common.Utility.Models.Config;
using Common.AOP;

namespace YourWebApiName.ApiServices.Extensions
{
    /// <summary>
    /// 第三方AutofacModule
    /// </summary>
    public class AutofacDefaultModule : Autofac.Module
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //AOP面向切面编程服务
            var aopServices = new List<Type>();
            var dbConfig = StaticConfig.AppSettings.ServiceCollectionExtension.DbConnection;
            if (dbConfig.MiniProfiler)
            {
                builder.RegisterType<MiniProfilerAop>();
                aopServices.Add(typeof(MiniProfilerAop));
            }

            /*
             可以直接引用程序集，每次都要重新生成很麻烦 上线时可以这么搞
             其他程序集只有继承接口才会自动注入,非依赖程序集的需要通过构造函数注入、如common
            */

#if TEST
            var basePath = Common.Utility.Models.Config.StaticConfig.ContentRootPath;//test使用
#else
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
#endif

#if DEBUG
            var assemblysServices = Assembly.LoadFrom(Path.Combine(basePath, "netstandard2.0/YourWebApiName.Services.dll"));
            var assemblysRepository = Assembly.LoadFrom(Path.Combine(basePath, "netstandard2.0/YourWebApiName.Repository.dll"));
#else
            var assemblysServices = Assembly.LoadFrom(Path.Combine(basePath, "YourWebApiName.Services.dll"));
            var assemblysRepository = Assembly.LoadFrom(Path.Combine(basePath, "YourWebApiName.Repository.dll"));
#endif

            builder.
                RegisterDefaultModuleImplementedInterfaces(
                aopServices.ToArray()
                , assemblysServices
                , assemblysRepository
                ,typeof(Common.NetCoreWebUtility.NetCoreWebUtilityAssembly).Assembly
                //, typeof(你需要注入的程序集的类).Assembly //或者使用上面的dll加载的方式
                //, typeof(Services.ServicesAssembly).Assembly
                //, typeof(Repository.RepositoryAssembly).Assembly
                );


            builder.RegisterDefaultModule(
                aopServices.ToArray(),
                typeof(Program).Assembly,
                typeof(Common.Utility.CommonUtilityAssembly).Assembly
                );


            //不使用这种方式  使用dll自动注入
            //builder.RegisterType<EmailWarningService>().As<IEmailWarningService>();

            base.Load(builder);
        }
    }
}
