using Dapper;
using IDataBase.Common;
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
        /// <summary>
        /// 创建连接对象委托  用于查看SQL
        /// </summary>
        private Func<DataBaseOption, IDbConnection> CreateConnectionAction;
        public MySqlDbContext(string connectionString) : base("`{0}`")
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// 自己创建
        /// </summary>
        /// <param name="createConnectionAction"></param>
        public MySqlDbContext(Func<DataBaseOption, IDbConnection> createConnectionAction) : base("`{0}`")
        {
            CreateConnectionAction = createConnectionAction;
        }

        public IDbConnection CreateConnection(DataBaseOption dataBaseOption = DataBaseOption.db0)
        {
            //不释放，方便事务处理
            IDbConnection conn;
            if (CreateConnectionAction != null)
            {
                conn = CreateConnectionAction(dataBaseOption);
            }
            else
            {
                conn = new MySqlConnection(ConnectionString);
            }
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

        public async Task<long> UpdateModelAsync<TTableModel>(TTableModel model, string[] whereFields = null, string[] notValidateFields = null) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                if (whereFields == null)
                {
                    whereFields = GetKeyName<TTableModel>().ToArray();
                }
                var whereSql = string.Join(" and ", whereFields.Select(item => $"{string.Format(FiledFormat, item)}=@{item}"));
                var strFieldNames = GetSqlUpdateString(model, whereFields);
                return await conn.ExecuteAsync($"UPDATE {GetTableName<TTableModel>()} SET {strFieldNames} WHERE {whereSql}", model);
            }
        }

        public async Task<long> UpdateAllModelAsync<TTableModel>(TTableModel model, string[] whereFields = null) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                if (whereFields == null)
                {
                    whereFields = GetKeyName<TTableModel>().ToArray();
                }
                var whereSql = string.Join(" and ", whereFields.Select(item => $"{string.Format(FiledFormat, item)}=@{item}"));
                var strFieldNames = GetSqlUpdateAllString(model, whereFields);
                return await conn.ExecuteAsync($"UPDATE {GetTableName<TTableModel>()} SET {strFieldNames} WHERE {whereSql}", model);
            }
        }

        public async Task<long> DeleteAsync<Tid, TTableModel>(Tid[] id) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                return await conn.ExecuteAsync($"DELETE FROM {GetTableName<TTableModel>()} WHERE {GetKeyName<TTableModel>().FirstOrDefault()} in @key", new { key = id });
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

        public async Task<long> CreateAsync<TTableModel>(TTableModel model, string[] notInFields) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                var fields = GetFields<TTableModel>(notInFields);
                var strFieldNames = string.Join(",", fields);
                var strParamFiledNames = "@" + string.Join(",@", fields);
                var sql = string.Format("INSERT INTO {0} ({1}) VALUES({2});select last_insert_id();", GetTableName<TTableModel>(), strFieldNames, strParamFiledNames);
                var multi = await conn.QueryMultipleAsync(sql, model);
                return multi.Read<long>().FirstOrDefault();
            }
        }

        public async Task<long> DeleteAsync<TTableModel>(TTableModel model, string[] notInFields = null) where TTableModel : class, new()
        {
            using (var conn = CreateConnection())
            {
                var fields = GetFields<TTableModel>();
                var strFieldNames = string.Join(",", fields);
                var sqlWhere = GetSqlQueryString(model, notInFields, string.Empty);
                var sql = $"DELETE FROM {GetTableName<TTableModel>()} WHERE 1=1 {sqlWhere}";
                return await conn.ExecuteAsync(sql, model);
            }
        }

        [Obsolete("已过期")]
        public Task<long> CreateToBulk<TTableModel>(TTableModel[] models)
        {
            throw new NotImplementedException();
        }
    }
}
