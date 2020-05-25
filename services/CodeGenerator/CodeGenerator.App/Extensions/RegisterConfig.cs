using CodeGenerator.App.Models;
using DataBase.DapperForSqlServer;
using System;
using System.Threading.Tasks;

namespace CodeGenerator.App.Extensions
{
    /// <summary>
    /// 注册JSON配置
    /// </summary>
    public static class RegisterConfig
    {
        /// <summary>
        /// 初始化系统配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        public static async Task InitConfig()
        {
            await InitAppSettings();
        }

        /// <summary>
        /// 初始化appsettins.json AppSettings 对象
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static async Task InitAppSettings()
        {
            StaticConfig.ContentRootPath = AppDomain.CurrentDomain.BaseDirectory;
            StaticConfig.AppSettings = await ConfigurationsModel.AppSettings.ReadJson<AppSettings>();

            var con = StaticConfig.AppSettings.DbConnection.SqlServerConnection;
            //StaticConfig.DbContext = new MySqlSqlSugarDbContext(con);
            StaticConfig.DbContext = new SqlServerDbContext(con);
        }
    }
}
