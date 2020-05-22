using CodeGenerator.App.DbModels;
using CodeGenerator.App.Models;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenerator.App.Repository
{
    public class TableRepository
    {
        public async Task<IEnumerable<TablesModel>> GetModelsAsync()
        {
            //var mySql = "select table_name,table_comment from information_schema.tables where table_schema=@dbName";
            //return await StaticConfig.DbContext.GetModelsAsync<TablesModel, object>(sqlServer, new { dbName = StaticConfig.AppSettings.DbConnection.DbName });
            using (var conn= StaticConfig.DbContext.CreateConnection())
            {
                var sqlServer = $"select name as table_name,name table_comment from {StaticConfig.AppSettings.DbConnection.DbName}.sys.tables ";//排序不需要生成的表名;a where a.name not like '%temp%' and a.name like 'tb_%'
                return await conn.QueryAsync<TablesModel>(sqlServer);
            }
        }
    }
}
