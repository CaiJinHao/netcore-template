using Dapper;
using IDataBase.DbExtensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataBase.DapperForMySql
{
    public class MySqlDbContext : DbContextAbstract,IMySqlDbContext
    {
        private string ConnectionString { get; set; }

        public string PrimaryKey => Guid.NewGuid().ToString("n");

        public MySqlDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            //不释放，方便事务处理
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            return conn;
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

        public async Task<bool> CreateAsync<TTableModel>(TTableModel[] models) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                var fields = GetFields<TTableModel>();
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
                var keyName = GetKeyName<TTableModel>();
                var sql = $"SELECT {strFieldNames} FROM {GetTableName<TTableModel>()} AS A WHERE {keyName}=@key";
                var models = await conn.QueryAsync<TTableModel>(sql, new { key = id });
                return models.FirstOrDefault();
            }
        }

        public async Task<long> UpdateModelAsync<Tid, TTableModel>(Tid id, TTableModel model) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                var keyName = GetKeyName<TTableModel>();
                var strFieldNames = GetSqlUpdateString(model);
                return await conn.ExecuteAsync($"UPDATE {GetTableName<TTableModel>()} SET {strFieldNames} WHERE {keyName}=@{keyName}", model);
            }
        }

        public async Task<long> DeleteAsync<Tid, TTableModel>(Tid[] id) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                return await conn.ExecuteAsync($"DELETE FROM {GetTableName<TTableModel>()} WHERE {GetKeyName<TTableModel>()} in @key", new { key = id });
            }
        }

        public async Task<long> DeleteAsync<TTableModel>(TTableModel model) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                var fields = GetFields<TTableModel>();
                var strFieldNames = string.Join(",", fields);
                var sqlWhere = GetSqlQueryString(model);
                var sql = $"DELETE FROM {GetTableName<TTableModel>()} WHERE 1=1 {sqlWhere}";
                return await conn.ExecuteAsync(sql, model);
            }
        }
    }
}
