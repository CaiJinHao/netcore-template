using Common.Utility.Models.Config;
using Common.Utility.Models.User;
using DataBase.DapperForSqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Profiling;

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
#if !DEBUG
                        return new SqlServerDbContext(dbConfig.ConnectionString, () =>
                        {
                            var connection = new System.Data.SqlClient.SqlConnection(dbConfig.ConnectionString);
                            return new StackExchange.Profiling.Data.ProfiledDbConnection(connection, MiniProfiler.Current);
                        });
#else
                        return new SqlServerDbContext(dbConfig.ConnectionString);
#endif
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
