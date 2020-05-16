using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Utility.Models.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using YourWebApiName.ApiServices.Extensions;

namespace ApiServices.Test
{
    public class DIBase
    {
        public IContainer container { get; set; }
        public DIBase()
        {
            StaticConfig.ContentRootPath = @"E:\Cngrain\IotCloudBox\trunk\src\CloudBoxServer\CloudBox.App";
            container = DICollections();

        }

        public IContainer DICollections()
        {

            IServiceCollection services = new ServiceCollection();
            //services.AddAutoMapper(typeof(Startup));
            services.AddAppServices();

            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacDefaultModule>();
            //builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();

            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            //var blogservice = ApplicationContainer.Resolve<IBlogArticleServices>();
            //var myContext = ApplicationContainer.Resolve<MyContext>();

            return ApplicationContainer;
        }

        //public ObjectId GetPrimariKey()
        //{
        //    return ObjectId.GenerateNewId(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));
        //}
    }
}
