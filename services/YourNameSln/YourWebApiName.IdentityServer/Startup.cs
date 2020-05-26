using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourWebApiName.IdentityServer.Extensions;
using YourWebApiName.IdentityServer.Models;
using YourWebApiName.IdentityServer.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace YourWebApiName.IdentityServer
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            StaticConfigModel.ContentRootPath = env.ContentRootPath;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHsts(opts => {
                opts.Preload = true;
                opts.IncludeSubDomains = true;
            });

            //注意必须要使用 dotnet 命令行启动  否则会爆rsa 文件错误
            //请求测试 路径 /.well-known/openid-configuration
            services.InitConfig(_s =>
                    {
                        using (var _service = _s.BuildServiceProvider())
                        {
                            IdentityConfigService.AppSettings = _service.GetService<IOptions<AppSettingsModel>>().Value;
                        }
                        return _s;
                    })
                    .AddIdentityServer()
                    .AddDeveloperSigningCredential(true)
                    //.AddDeveloperSigningCredential(true, StaticConfig.ContentRootPath + "/tempkey.rsa")
                    //.AddInMemoryIdentityResources(IdentityConfigService.GetIdentityResourceResources())
                    .AddInMemoryApiResources(IdentityConfigService.GetApiResources(IdentityConfigService.AppSettings.ApiResource))
                    .AddInMemoryClients(IdentityConfigService.GetClients(IdentityConfigService.AppSettings.Clients))
                    .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()  //先执行  验证用户名密码是否正确
                    //.AddProfileService<CustomProfileService>()//后执行
                    ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettingsModel> appSettingsOptions)
        {
            StaticConfigModel.AppSettings = appSettingsOptions.Value;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseIdentityServer();
            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                //endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
