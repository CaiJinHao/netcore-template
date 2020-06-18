using Dapper;
using IDataBase.DbExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataBase.DapperForSqlServer
{
    /*
     = 比 like 快
     */

    /// <summary>
    /// 该类可以继承mysqlDbContext，如果有不一样的可以重写，但是需要引入没有必要的MySql.Data，可以衡量一下是否需要
    /// </summary>
    public class SqlServerDbContext : DbContextAbstract, ISqlServerDbContext
    {
        private string ConnectionString { get; set; }

        public string PrimaryKey => Guid.NewGuid().ToString("n");
        /// <summary>
        /// 创建连接对象委托  用于查看SQL
        /// </summary>
        private Func<IDbConnection> CreateConnectionAction;
        public SqlServerDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public SqlServerDbContext(string connectionString,Func<IDbConnection> createConnectionAction)
        {
            ConnectionString = connectionString;
            CreateConnectionAction = createConnectionAction;
        }

        public IDbConnection CreateConnection()
        {
            if (CreateConnectionAction!=null)
            {
                return CreateConnectionAction();
            }
            else
            {
                var conn = new SqlConnection(ConnectionString);
                conn.Open();
                return conn;
            }
        }

        public async Task<bool> CreateAsync<TTableModel>(TTableModel model) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                var fields = GetFields<TTableModel>();
                var strFieldNames = string.Join(",", fields);
                var strParamFiledNames = "@" + string.Join(",@", fields);
                var sql = string.Format("INSERT INTO {0} ({1}) VALUES({2})", GetTableName<TTableModel>(), strFieldNames, strParamFiledNames);
                var i = await conn.ExecuteAsync(sql, model);
                return i > 0;
            }
        }

        public async Task<long> CreateAsync<TTableModel>(TTableModel model,string[] notInFields) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                var fields = GetFields<TTableModel>(notInFields);
                var strFieldNames = string.Join(",", fields);
                var strParamFiledNames = "@" + string.Join(",@", fields);
                var sql = string.Format("INSERT INTO {0} ({1}) VALUES({2});select SCOPE_IDENTITY() id;", GetTableName<TTableModel>(), strFieldNames, strParamFiledNames);
                var multi = await conn.QueryMultipleAsync(sql, model);
                return multi.Read<long>().FirstOrDefault();
            }
        }

        public async Task<bool> CreateAsync<TTableModel>(TTableModel[] models, string[] notInFields = null) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                var fields = GetFields<TTableModel>(notInFields);
                var strFieldNames = string.Join(",", fields);
                var strParamFiledNames = "@" + string.Join(",@", fields);
                var sql = string.Format("INSERT INTO {0} ({1}) VALUES({2})", GetTableName<TTableModel>(), strFieldNames, strParamFiledNames);
                var i = await conn.ExecuteAsync(sql, models);
                return i > 0;
            }
        }

        public async Task<TTableModel> GetModelAsync<Tid, TTableModel>(Tid id, IEnumerable<string> fields = null) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                var strFieldNames = GetFieldsToString<TTableModel>("A", fields);
                var keyName = GetKeyName<TTableModel>().FirstOrDefault();
                var sql = $"SELECT {strFieldNames} FROM {GetTableName<TTableModel>()} AS A WHERE {keyName}=@key";
                var models = await conn.QueryAsync<TTableModel>(sql, new { key = id });
                return models.FirstOrDefault();
            }
        }

        public async Task<long> UpdateModelAsync<TTableModel>(TTableModel model, string[] whereFields = null) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                string whereSql = string.Empty;
                if (whereFields == null)
                {
                    whereFields = GetKeyName<TTableModel>().ToArray();
                }
                whereSql = string.Join(" and ", whereFields.Select(item => $"[{item}]=@{item}"));
                var strFieldNames = GetSqlUpdateString(model, whereFields);
                return await conn.ExecuteAsync($"UPDATE {GetTableName<TTableModel>()} SET {strFieldNames} WHERE {whereSql}", model);
            }
        }

        public async Task<long> DeleteAsync<Tid, TTableModel>(Tid[] id) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                var keyName = GetKeyName<TTableModel>().FirstOrDefault();
                return await conn.ExecuteAsync($"DELETE FROM {GetTableName<TTableModel>()} WHERE {keyName} in @key", new { key = id });
            }
        }

        public async Task<long> DeleteAsync<TTableModel>(TTableModel model, string[] notInFields = null) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                var fields = GetFields<TTableModel>();
                var strFieldNames = string.Join(",", fields);
                var sqlWhere = GetSqlQueryString(model,notInFields,string.Empty);
                var sql = $"DELETE FROM {GetTableName<TTableModel>()} WHERE 1=1 {sqlWhere}";
                return await conn.ExecuteAsync(sql, model);
            }
        }
    }
}
