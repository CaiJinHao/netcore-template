using CodeGenerator.App.DbModels;
using CodeGenerator.App.Models;
using CodeGenerator.App.Models.Enums;
using Dapper;
using DataBase.DapperForMySql;
using DataBase.DapperForSqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.App.Repository
{
    public class ColumnRepsitory
    {
        public async Task<IEnumerable<ColumnsModel>> GetModelsAsync(string tableName)
        {
            IDbConnection conn = null;
            var dbconn = StaticConfig.AppSettings.DbConnection;
            var querySql = string.Empty;
            switch (dbconn.DbType)
            {
                case Models.Enums.EnumDbType.MySql:
                    {
                        conn = new MySqlDbContext(dbconn.MySqlConnection).CreateConnection();
                        querySql = $"select column_name,ordinal_position,is_nullable,data_type,character_maximum_length,column_key,column_comment from information_schema.COLUMNS where table_schema='{StaticConfig.AppSettings.DbConnection.DbName}' and table_name = '{tableName}'  order by ordinal_position";
                        //return await StaticConfig.DbContext.GetModelsAsync<TablesModel, object>(sqlServer, new { dbName = StaticConfig.AppSettings.DbConnection.DbName });
                    }
                    break;
                case Models.Enums.EnumDbType.SqlServer:
                    {
                        conn = new SqlServerDbContext(dbconn.SqlServerConnection).CreateConnection();
                        querySql = $@"SELECT   
        col.colorder AS ordinal_position,  
        col.name AS column_name ,  
        ISNULL(ep.[value], col.name) AS column_comment,  
        t.name AS data_type,  
        col.length AS character_maximum_length,  
        ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) AS number_length ,  
        CASE WHEN COLUMNPROPERTY(col.id, col.name, 'IsIdentity') = 1 THEN 'YES'  
             ELSE 'NO'  
        END AS is_identity,  
        CASE WHEN EXISTS ( SELECT   1  
                           FROM     dbo.sysindexes si  
                                    INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id  
                                                              AND si.indid = sik.indid  
                                    INNER JOIN dbo.syscolumns sc ON sc.id = sik.id  
                                                              AND sc.colid = sik.colid  
                                    INNER JOIN dbo.sysobjects so ON so.name = si.name  
                                                              AND so.xtype = 'PK'  
                           WHERE    sc.id = col.id  
                                    AND sc.colid = col.colid ) THEN 'PRI'  
             ELSE CASE col.colid when 1 then 'PRI' else '' END
        END AS column_key,  
        CASE WHEN col.isnullable = 1 THEN 'YES'  
             ELSE 'NO'  
        END AS is_nullable,  
        ISNULL(comm.text, '') AS default_value
FROM    dbo.syscolumns col  
        LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype  
        inner JOIN dbo.sysobjects obj ON col.id = obj.id  
                                         AND obj.xtype = 'U'  
                                         AND obj.status >= 0  
        LEFT  JOIN dbo.syscomments comm ON col.cdefault = comm.id  
        LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id  
                                                      AND col.colid = ep.minor_id  
                                                      AND ep.name = 'MS_Description'  
        LEFT  JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id  
                                                         AND epTwo.minor_id = 0  
                                                         AND epTwo.name = 'MS_Description'  
WHERE   obj.name = '{tableName}'
ORDER BY col.colorder";//解决了没有主键的问题，没有主见按第一个字段查询
                    }
                    break;
                default:
                    break;
            }
            return await conn.QueryAsync<ColumnsModel>(querySql);
        }
    }
}
