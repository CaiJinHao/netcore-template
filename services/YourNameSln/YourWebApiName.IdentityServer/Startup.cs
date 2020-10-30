using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YourWebApiName.IdentityServer.Extensions;
using YourWebApiName.IdentityServer.Models;
using YourWebApiName.IdentityServer.Service;

namespace YourWebApiName.IdentityServer
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            StaticConfigModel.ContentRootPath = env.ContentRootPath;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddHsts(opts => {
            //    opts.Preload = true;
            //    opts.IncludeSubDomains = true;
            //});

            //报错，请清除掉tempkey.rsa再启动
            //请求测试 路径 /.well-known/openid-configuration
            services.InitConfig(_s =>
                    {
                        using (var _service = _s.BuildServiceProvider())
                        {
                            IdentityConfigService.AppSettings = _service.GetService<IOptions<AppSettingsModel>>().Value;
                            StaticConfigModel.AppSettings = IdentityConfigService.AppSettings;
                        }
                        return _s;
                    })
                    .AddIdentityServer()
                    //.AddSigningCredential(new X509Certificate2(path, Configuration["Certificates:Password"]))
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //StaticConfigModel.AppSettings = appSettingsOptions.Value;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            //app.UseHsts();
            //app.UseHttpsRedirection();


            app.UseMvc();
        }
    }
}
