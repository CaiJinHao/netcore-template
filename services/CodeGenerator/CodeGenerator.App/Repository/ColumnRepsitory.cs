using CodeGenerator.App.DbModels;
using CodeGenerator.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.App.Repository
{
    public class ColumnRepsitory
    {
        public async Task<IEnumerable<ColumnsModel>> GetModelsAsync(string tableName)
        {
            var sql = "select table_name,column_name,ordinal_position,is_nullable,data_type,character_maximum_length,column_key,column_comment from information_schema.COLUMNS where table_schema=@dbName and table_name = @tableName  order by ordinal_position";
            return await StaticConfig.DbContext.GetModelsAsync<ColumnsModel, object>(sql, new { dbName = StaticConfig.AppSettings.DbConnection.DbName, tableName = tableName });
        }
    }
}
