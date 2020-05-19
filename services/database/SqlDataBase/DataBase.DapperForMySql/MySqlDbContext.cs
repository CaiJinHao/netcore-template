using Dapper;
using DataBase.IDataBase;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DataBase.DapperForMySql
{
    public class MySqlDbContext : DbContextAbstract, IMySqlDbContext
    {
        private string ConnectionString { get; set; }

        public string PrimaryKey => Guid.NewGuid().ToString("n");

        public MySqlDbContext(string connectionString) { 
            ConnectionString = connectionString;
        }
     
        public IDbConnection CreateConnection()
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                return conn;
            }
        }

        public async Task<bool> CreateAsync<TTableModel>(TTableModel model) where TTableModel : class, new()
        {
            var fields = GetFields<TTableModel>();
            var strFieldNames = string.Join(",", fields);
            var strParamFiledNames = "@" + string.Join(",@", fields);
            var sql = string.Format("INSERT INTO {0} ({1}) VALUES({2})", GetTableName<TTableModel>(), strFieldNames, strParamFiledNames);
            var i = await CreateConnection().ExecuteAsync(sql, model);
            return i > 0;
        }

        public async Task<bool> CreateAsync<TTableModel>(TTableModel[] models) where TTableModel : class, new()
        {
            var fields = GetFields<TTableModel>();
            var strFieldNames = string.Join(",", fields);
            var strParamFiledNames = "@" + string.Join(",@", fields);
            var sql = string.Format("INSERT INTO {0} ({1}) VALUES({2})", GetTableName<TTableModel>(), strFieldNames, strParamFiledNames);
            var i = await CreateConnection().ExecuteAsync(sql, models);
            return i > 0;
        }

        public async Task<TTableModel> GetModelAsync<Tid, TTableModel>(Tid id) where TTableModel : class, new()
        {
            var fields = GetFields<TTableModel>();
            var strFieldNames = string.Join(",", fields);
            var keyName = GetKeyName<TTableModel>();
            var sql = $"SELECT {strFieldNames} FROM {GetTableName<TTableModel>()} AS A WHERE {keyName}=@key";
            var models= await CreateConnection().QueryAsync<TTableModel>(sql, new { key = id });
            return models.FirstOrDefault();
        }

        public async Task<IEnumerable<TTableModel>> GetModelsAsync<TTableModel>() where TTableModel : class, new()
        {
            var sql = $"SELECT * FROM {GetTableName<TTableModel>()}";
            return await CreateConnection().QueryAsync<TTableModel>(sql);
        }

        public async Task<long> UpdateModelAsync<Tid, TTableModel>(Tid id, TTableModel model) where TTableModel : class, new()
        {
            var keyName = GetKeyName<TTableModel>();
            var fields = GetFields<TTableModel>(new List<string>() { keyName });
            var strFieldNames = string.Empty;
            foreach (var item in fields)
            {
                strFieldNames += item + "=@" + item + ",";
            }
            strFieldNames = strFieldNames.Trim(',');
            return await CreateConnection().ExecuteAsync($"UPDATE {GetTableName<TTableModel>()} SET {strFieldNames} WHERE {keyName}=@{keyName}", model);
        }

        public async Task<long> DeleteAsync<Tid, TTableModel>(Tid[] id) where TTableModel : class, new()
        {
            return await CreateConnection().ExecuteAsync($"DELETE FROM {GetTableName<TTableModel>()} WHERE {GetKeyName<TTableModel>()} = @key", new { key = id });
        }

        public async Task<IEnumerable<TTableModel>> GetModelsAsync<TTableModel, TParameter>(string sql, TParameter modelParameter) where TTableModel : class, new()
        {
            return await CreateConnection().QueryAsync<TTableModel>(sql,modelParameter);
        }

        public TTableModel GetModel<Tid, TTableModel>(Tid id) where TTableModel : class, new()
        {
            var fields = GetFields<TTableModel>();
            var strFieldNames = string.Join(",", fields);
            var keyName = GetKeyName<TTableModel>();
            var sql = $"SELECT {strFieldNames} FROM {GetTableName<TTableModel>()} AS A WHERE {keyName}=@key";
            var models =  CreateConnection().Query<TTableModel>(sql, new { key = id });
            return models.FirstOrDefault();
        }

        public Task<long> DeleteAsync<TTableModel>(TTableModel model) where TTableModel : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
