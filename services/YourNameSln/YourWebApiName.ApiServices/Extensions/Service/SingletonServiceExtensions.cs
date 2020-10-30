using Common.Utility.Models.Config;
using DataBase.DapperForMySql;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System.Data.Common;

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
                    .AddSingleton<IMySqlDbContext>(s =>
                    {
                        return new MySqlDbContext((dbOption) =>
                        {
                            DbConnection connection;
                            switch (dbOption)
                            {
                                case IDataBase.Common.DataBaseOption.db1:
                                    {
                                        //connection = new System.Data.SqlClient.SqlConnection(dbConfig.ConnectionStringDb1);
                                        connection = new MySqlConnection(dbConfig.ConnectionStringDb1);
                                    }
                                    break;
                                case IDataBase.Common.DataBaseOption.db0:
                                default:
                                    {
                                        //connection = new System.Data.SqlClient.SqlConnection(dbConfig.ConnectionString);
                                        connection = new MySqlConnection(dbConfig.ConnectionString);
                                    }
                                    break;
                            }
                            if (dbConfig.MiniProfiler)
                            {
                                return new ProfiledDbConnection(connection, MiniProfiler.Current);
                            }
                            else
                            {
                                return connection;
                            }
                        });
                    })
                    //.AddSingleton<ISqlServerDbContext>(s =>
                    //{
                    //    return new SqlServerDbContext((dbOption) =>
                    //    {
                    //        DbConnection connection;
                    //        switch (dbOption)
                    //        {
                    //            case IDataBase.Common.DataBaseOption.db1:
                    //                {
                    //                    //connection = new System.Data.SqlClient.SqlConnection(dbConfig.ConnectionStringDb1);
                    //                    connection = new MySqlConnection(dbConfig.ConnectionStringDb1);
                    //                }
                    //                break;
                    //            case IDataBase.Common.DataBaseOption.db0:
                    //            default:
                    //                {
                    //                    //connection = new System.Data.SqlClient.SqlConnection(dbConfig.ConnectionString);
                    //                    connection = new MySqlConnection(dbConfig.ConnectionString);
                    //                }
                    //                break;
                    //        }
                    //        if (dbConfig.MiniProfiler)
                    //        {
                    //            return new ProfiledDbConnection(connection, MiniProfiler.Current);
                    //        }
                    //        else
                    //        {
                    //            return connection;
                    //        }
                    //    });
                    //})
                    //.AddSingleton<IMySqlDbContext>(s =>
                    //{
                    //    return new MySqlDbContext(dbConfig.ConnectionString);
                    //})
                    //.AddSingleton<IMongoDbContext>(s =>
                    //{
                    //    var mongoDb = dbConfig.MongoDB;
                    //    return new MongoDbContext(mongoDb.ConnectionUrl, mongoDb.DbName);
                    //})
                    //example 全局服务
                    //.AddSingleton<IGrainThermonetryTaskTimingService, GrainThermonetryTaskTimingService>()
                    ;
        }
    }
}
