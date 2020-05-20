using CodeGenerator.App.DbModels;
using CodeGenerator.App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenerator.App.Repository
{
    public class TableRepository
    {
        public async Task<IEnumerable<TablesModel>> GetModelsAsync()
        {
            //var mySql = "select table_name,table_comment from information_schema.tables where table_schema=@dbName";
            var sqlServer = "select name as table_name,name table_comment from MonitorOnlineDB.sys.tables;";
            return await StaticConfig.DbContext.GetModelsAsync<TablesModel, object>(sqlServer, new { dbName = StaticConfig.AppSettings.DbConnection.DbName });
        }
    }
}
