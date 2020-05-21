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
            //var mySql = "select table_name,column_name,ordinal_position,is_nullable,data_type,character_maximum_length,column_key,column_comment from information_schema.COLUMNS where table_schema=@dbName and table_name = @tableName  order by ordinal_position";
            //return await StaticConfig.DbContext.GetModelsAsync<ColumnsModel, object>(sqlServer, new { dbName = StaticConfig.AppSettings.DbConnection.DbName, tableName = tableName });

            var sqlServer = $@"select 
c.name table_name,a.name column_name,a.colorder ordinal_position,b.name data_type,b.length character_maximum_length,a.name column_comment,CASE a.colid when 1 then 'PRI' else '' END column_key,CASE a.isnullable when 0 then 'NO' ELSE 'YES' END is_nullable
from syscolumns a 
join systypes b on a.xtype=b.xusertype
join sysobjects c on c.id=a.id 
Where a.id=object_Id('{tableName}') order by a.colorder
            ";
            return await StaticConfig.DbContext.GetModelsAsync<ColumnsModel,object>(sqlServer,new { });
        }
    }
}
