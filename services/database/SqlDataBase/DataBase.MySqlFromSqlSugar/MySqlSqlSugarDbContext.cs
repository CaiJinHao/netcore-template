using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utility.Extension;
using DataBase.IDataBase;

namespace DataBase.MySqlFromSqlSugar
{
    /// <summary>
    /// TODO:待测试（测试不通过，查询时映射错误）
    /// </summary>
    public class MySqlSqlSugarDbContext : DbContextAbstract,IMySqlSqlSugarDbContext
    {
        private string ConnectionString { get; set; }
        private ILogger Logger { get; set; }

        public string PrimaryKey => Guid.NewGuid().ToString("n");

        public MySqlSqlSugarDbContext(string connectionString) { 
            ConnectionString = connectionString;
            Logger = typeof(MySqlSqlSugarDbContext).Logger();
        }
        public SqlSugarClient CreateConnection()
        {
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = SqlSugar.DbType.MySql,
                IsAutoCloseConnection = false
            });
            //Print sql
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                //Console.WriteLine();
            };
            return db;
        }

        public async Task<bool> CreateAsync<TTableModel>(TTableModel model) where TTableModel : class, new()
        {
            try
            {
                await CreateConnection().Insertable(model).ExecuteCommandAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public async Task<bool> CreateAsync<TTableModel>(TTableModel[] models) where TTableModel : class, new()
        {
            try
            {
                await CreateConnection().Insertable(models).ExecuteCommandAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public async Task<long> DeleteAsync<Tid, TTableModel>(Tid[] id) where TTableModel : class, new()
        {
            try
            {
               return await CreateConnection().Deleteable<TTableModel>().In(id).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return 0;
            }
        }

        public async Task<TTableModel> GetModelAsync<Tid, TTableModel>(Tid id) where TTableModel : class, new()
        {
            var keyName = GetKeyName<TTableModel>();
            return await CreateConnection().SqlQueryable<TTableModel>($"select * from {GetTableName<TTableModel>()}")
                 .Where($"{keyName}=@key", new { key = id }).FirstAsync();
        }

        public TTableModel GetModel<Tid, TTableModel>(Tid id) where TTableModel : class, new()
        {
            var keyName = GetKeyName<TTableModel>();
            return CreateConnection().SqlQueryable<TTableModel>($"select * from {GetTableName<TTableModel>()}")
                 .Where($"{keyName}=@key", new { key = id }).First();
        }

        public async Task<IEnumerable<TTableModel>> GetModelsAsync<TTableModel>() where TTableModel : class, new()
        {
           return await CreateConnection().Queryable<TTableModel>().ToListAsync();
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

            return await CreateConnection().Ado
                .ExecuteCommandAsync($"UPDATE {GetTableName<TTableModel>()} SET {strFieldNames} WHERE {keyName}=@{keyName}", model);
        }

        public async Task<IEnumerable<TTableModel>> GetModelsAsync<TTableModel, TParameter>(string sql, TParameter modelParameter) where TTableModel : class, new()
        {
           return await CreateConnection().Ado.SqlQueryAsync<TTableModel>(sql,modelParameter);
        }

        public Task<long> UpdateModelAsync<Tid, TTableModel>(Tid id, TTableModel model, string notFieldRegex = "^_id") where TTableModel : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<long> DeleteAsync<TTableModel>(TTableModel model) where TTableModel : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
