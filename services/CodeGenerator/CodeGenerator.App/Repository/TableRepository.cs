using CodeGenerator.App.DbModels;
using CodeGenerator.App.Models;
using DataBse.MySqlFromDapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.App.Repository
{
    public class TableRepository
    {
        public async Task<IEnumerable<TablesModel>> GetModelsAsync()
        {
            var sql = "select table_name,table_comment from information_schema.tables where table_schema=@dbName";
            return await StaticConfig.DbContext.GetModelsAsync<TablesModel, object>(sql, new { dbName = StaticConfig.AppSettings.DbConnection.DbName });
        }
    }
}
