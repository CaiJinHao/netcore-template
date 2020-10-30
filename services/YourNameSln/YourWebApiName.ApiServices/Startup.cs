using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Utility.Models.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using System;
using YourWebApiName.ApiServices.Extensions;
using YourWebApiName.ApiServices.Extensions.Middleware;

namespace YourWebApiName.ApiServices
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 启动构造函数DI
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            StaticConfig.ContentRootPath = env.ContentRootPath;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAppServices();

            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacDefaultModule>();
            //builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();

            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            //将AutoFac反馈到管道中
            return new AutofacServiceProvider(ApplicationContainer);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env
            //, IHost host
            , IApiVersionDescriptionProvider provider)
        {

            app
                .UseSwaggerMiddleware(provider)
               .UseMiddleware();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc(routes =>
            {
                //默认控制器
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            /*
             * // 使用服务提供者获取服务
            using (var container = host.Services.CreateScope())
            {
                container.ServiceProvider.GetService<IEmailWarningService>();
            }*/
        }
    }
}
