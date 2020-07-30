using CodeGenerator.App.DbModels;
using CodeGenerator.App.Models;
using Dapper;
using DataBase.DapperForMySql;
using DataBase.DapperForSqlServer;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CodeGenerator.App.Repository
{
    public class TableRepository
    {
        public async Task<IEnumerable<TablesModel>> GetModelsAsync()
        {
            IDbConnection conn = null;
            var dbconn = StaticConfig.AppSettings.DbConnection;
            var querySql = string.Empty;
            switch (dbconn.DbType)
            {
                case Models.Enums.EnumDbType.MySql:
                    {
                        conn = new MySqlDbContext(dbconn.MySqlConnection).CreateConnection();
                        querySql = $"select table_name,table_comment from information_schema.tables where table_schema='{StaticConfig.AppSettings.DbConnection.DbName}'";
                        //return await StaticConfig.DbContext.GetModelsAsync<TablesModel, object>(sqlServer, new { dbName = StaticConfig.AppSettings.DbConnection.DbName });
                    }
                    break;
                case Models.Enums.EnumDbType.SqlServer:
                    {
                        conn = new SqlServerDbContext(dbconn.SqlServerConnection).CreateConnection();
                        querySql = $"select name as table_name,name table_comment from {StaticConfig.AppSettings.DbConnection.DbName}.sys.tables a where 1=1 {StaticConfig.AppSettings.Template.TableSqlWhere}";//排序不需要生成的表名; and a.name not like '%temp%' and a.name like 'tb_%'
                    }
                    break;
                default:
                    break;
            }
            return await conn.QueryAsync<TablesModel>(querySql);
        }
    }
}
