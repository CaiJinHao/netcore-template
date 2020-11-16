

using Common.Utility.Extension;
using Common.Utility.Models.HttpModels;
using DataBase.DapperForMySql;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourWebApiName.IRepository.IDbRepository;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;

namespace YourWebApiName.MongoRepository.DbRepository
{
    public class SysUsersRepository : ISysUsersRepository
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string tableName { get; set; }

       /// <summary>
       /// 资源 DbRepository 系统_用户
       /// </summary>
       public SysUsersRepository()
       {
            tableName = "sys_users";
       }

        public IMySqlDbContext DbContext { get; set; }

        public async Task<bool> CreateAsync(SysUsersModel model)
        {
          model.user_time = DateTime.Now.Utc();
            return await DbContext.CreateAsync(model);
        }

        public async Task<bool> CreateAsync(SysUsersModel[] models)
        {
            return await DbContext.CreateAsync(models);
        }

        public async Task<long> DeleteAsync(string[] id)
        {
            return await DbContext.DeleteAsync<string, SysUsersModel>(id);
        }

        public async Task<long> DeleteAsync(SysUsersModel model)
        {
            return await DbContext.DeleteAsync(model);
        }

        public async Task<SysUsersModel> GetModelAsync(string id, IEnumerable<string> fields = null)
        {
            return await DbContext.GetModelAsync<string, SysUsersModel>(id,fields);
        }

        private string GetQuery(SysUsersRequestModel queryParameter, string table = null)
        {
            switch (table)
            {
                default:
                    {
                        var sb = new StringBuilder();
                        //如果需要模糊查询请处理参数值添加%
                        sb.Append(DbContext.GetSqlQueryString<SysUsersModel>(queryParameter));
                        return sb.ToString();
                    }
            }
        }

        public async Task<IEnumerable<SysUsersModel>> GetCurrentModelsAsync(SysUsersRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            using (var conn = DbContext.CreateConnection())
            {
                var strWhere = GetQuery(queryParameter);
            var strFieldNames = DbContext.GetFieldsToString<SysUsersModel>("b1", fields);
                var dataQuery = $"SELECT {strFieldNames} FROM {tableName} b1 WHERE 1=1 {strWhere} ORDER BY b1.user_id ASC";
                return await conn.QueryAsync<SysUsersModel>(dataQuery, queryParameter);
            }
        }
        /// <summary>
        /// 角色表可用的角色 内联接 不需要直接置为空
        /// </summary>
        private const string joinTable = "join (select * from sys_roles b2 where b2.role_is_enable=1) b2_result on b1_result.role_id=b2_result.role_id ";
        /// <summary>
        /// 连接表需要查询字段 不需要直接置为空
        /// </summary>
        private const string joinFiles = ",b2_result.role_name";
        public async Task<IEnumerable<SysUsersResponeModel>> GetModelsAsync(SysUsersRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            using (var conn = DbContext.CreateConnection())
            {
                var strWhere = GetQuery(queryParameter);
            var strFieldNames = DbContext.GetFieldsToString<SysUsersModel>("b1", fields);
                var dataQuery = $"SELECT b1_result.*{joinFiles} FROM (SELECT {strFieldNames} FROM {tableName}  b1 WHERE 1=1 {strWhere}) b1_result {joinTable} ORDER BY b1_result.user_id ASC";//内查询，可以做连接查询 直接join
                return await conn.QueryAsync<SysUsersResponeModel>(dataQuery, queryParameter);
            }
        }

        public async Task<IEnumerable<SysUsersResponeModel>> GetModelsAsync(PagingModel pagingModel, SysUsersRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            var strWhere = GetQuery(queryParameter);
            var strFieldNames = DbContext.GetFieldsToString<SysUsersModel>("b1", fields);
            var querySql = "SELECT {0} " + $"FROM (SELECT {strFieldNames} FROM {tableName}  b1 WHERE 1=1 {strWhere} ORDER BY user_id ASC) b1_result {joinTable}";//内查询，可以做连接查询 直接join
            var countQuery = string.Format(querySql, "COUNT(1)");

            var pagingSql = $" LIMIT {pagingModel.StartIndex()},{pagingModel.PageSize}";//分页
            var dataQuery = string.Format(querySql, $"b1_result.*{joinFiles}") + pagingSql;

            using (var conn = DbContext.CreateConnection())
            {
                pagingModel.TotalCount = await conn.ExecuteScalarAsync<long>(countQuery,queryParameter);
                return await conn.QueryAsync<SysUsersResponeModel>(dataQuery, queryParameter); 
            }
        }

        public async Task<long> UpdateModelAsync(SysUsersModel model)
        {
            return await DbContext.UpdateModelAsync( model);
        }

        public async Task<long> UpdateAllModelAsync(SysUsersModel model)
        {
            return await DbContext.UpdateAllModelAsync( model);
        }
    }
}
