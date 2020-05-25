using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataBase.DapperForMySql
{
    public class MySqlDbContextOld
    {
        private string connectionString { get; set; }

        public MySqlDbContextOld(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                return conn;
            }
        }

        public async Task<long> InsertAsync<TParameter>(string tableName, IEnumerable<string> FieldNames, TParameter objParams)
        {
            return await InsertArrayAsync(tableName,FieldNames,objParams);
        }

        public async Task<long> InsertArrayAsync<T>(string tableName, IEnumerable<string> FieldNames, params T[] objParams)
        {
            var strFieldNames = string.Join(",", FieldNames);
            var strParamFiledNames = "@" + string.Join(",@", FieldNames);
            var sql = string.Format("INSERT INTO {0} ({1}) VALUES({2})", tableName, strFieldNames, strParamFiledNames);
            return await CreateConnection().ExecuteAsync(sql, objParams);
        }

        public async Task<long> IdentityInsertAsync<T>(string tableName, IEnumerable<string> FieldNames, T objParams)
        {
            var strFieldNames = string.Join(",", FieldNames);
            var strParamFiledNames = "@" + string.Join(",@", FieldNames);
            var sql = string.Format("INSERT INTO {0} ({1}) VALUES({2});SELECT LAST_INSERT_ID() AS id;", tableName, strFieldNames, strParamFiledNames);
            var multi = await CreateConnection().QueryAsync(sql, objParams);
            return Convert.ToInt64(multi.FirstOrDefault().id);
        }

        public async Task<int> DeleteAsync<T>(string tableName, string fieldName, params T[] _fieldNameValueIEnumerable)
        {
            var sql = string.Format("DELETE FROM {0} WHERE {1} IN @idIEnumerable", tableName, fieldName);
            return await CreateConnection().ExecuteAsync(sql, new { idIEnumerable = _fieldNameValueIEnumerable });
        }

        public async Task<int> DeleteByFieldAsync<T>(string tableName, string fieldName, T fieldNameValue)
        {
            var sql = string.Format("DELETE FROM {0} WHERE A.{1}=@_val", tableName, fieldName);
            return await CreateConnection().ExecuteAsync(sql, new { _val = fieldNameValue });
        }

        public async Task<int> UpdateAsync<T>(string tableName, IEnumerable<string> _fieldNames, string strWhereSql, T _objParams)
        {
            var strFieldNames = string.Empty;
            foreach (var item in _fieldNames)
            {
                strFieldNames += item + "=@" + item + ",";
            }
            strFieldNames = strFieldNames.Trim(',');
            var sql = string.Format("UPDATE {0} SET  {1}  WHERE {2}", tableName, strFieldNames, strWhereSql);
            return await CreateConnection().ExecuteAsync(sql, _objParams);
        }

        public async Task<IEnumerable<TReulst>> GetIEnumerableBySqlWhereAsync<TReulst, T>(string tableName, string filedNames, string strWhereSql, T _objParams)
        {
            var sql = string.Format("SELECT {0} FROM {1} AS A WHERE {2}", filedNames, tableName, strWhereSql);
            return await GetIEnumerableAsync<TReulst, T>(sql, _objParams);
        }

        public async Task<IEnumerable<TReulst>> GetIEnumerableAsync<TReulst, T>(string sql, T _objParams)
        {
            return await CreateConnection().QueryAsync<TReulst>(sql, _objParams);
        }

        public async Task<bool> ExistByIdAsync<T>(string tableName, string filedName, T _filedNameValue)
        {
            var sql = string.Format("SELECT COUNT(1) FROM {0} AS A WHERE A.{1}=@_val", tableName, filedName);
            return await ExistBySqlAsync(sql, new { _val = _filedNameValue });
        }

        public async Task<bool> ExistBySqlAsync<T>(string sql, T valueObj)
        {
            var _exist = await ExecuteScalarAsync<T>(sql, valueObj);
            if (_exist > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<int> ExecuteAsync<T>(string sql, T _objParams)
        {
            return await CreateConnection().ExecuteAsync(sql, _objParams);
        }

        public async Task<long> ExecuteScalarAsync<T>(string sql, T valueObj)
        {
            return await CreateConnection().ExecuteScalarAsync<long>(sql, valueObj);
        }

        
    }
}
