using Common.Utility.Models.Config;
using Common.Utility.Models.User;
using DataBase.DapperForSqlServer;
using DataBase.IDataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.ApiServices.Extensions.Service
{
    /// <summary>
    /// 自定义单例
    /// </summary>
    public static class SingletonServiceExtensions
    {
        /// <summary>
        /// 添加自定义单例
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddCustomSingletonService(this IServiceCollection services)
        {
            //生命周期：整个应用程序有效
            var dbConfig = StaticConfig.AppSettings.ServiceCollectionExtension.DbConnection;
            return services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()//全局HTTP
                    .AddSingleton<IHttpInfo, HttpInfo>()
                    .AddSingleton<ISqlServerDbContext>(s =>
                    {
                        return new SqlServerDbContext(dbConfig.ConnectionString);
                    })
                    //.AddSingleton<IMySqlDbContext>(s =>
                    //{
                    //    return new MySqlDbContext(dbConfig.ConnectionString);
                    //})
                    //.AddSingleton<IMongoDbContext>(s =>
                    //{
                    //    var mongoDb = dbConfig.MongoDB;
                    //    return new MongoDbContext(mongoDb.ConnectionUrl, mongoDb.DbName);
                    //})
                    //example 后台服务
                    //.AddSingleton<IGrainThermonetryTaskTimingService, GrainThermonetryTaskTimingService>()
                    ;
        }
    }
}
