

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
    public class SysRolesRepository : ISysRolesRepository
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string tableName { get; set; }

       /// <summary>
       /// 资源 DbRepository 系统_角色
       /// </summary>
       public SysRolesRepository()
       {
            tableName = "sys_roles";
       }

        public IMySqlDbContext DbContext { get; set; }

        public async Task<bool> CreateAsync(SysRolesModel model)
        {
            return await DbContext.CreateAsync(model);
        }

        public async Task<bool> CreateAsync(SysRolesModel[] models)
        {
            return await DbContext.CreateAsync(models);
        }

        public async Task<long> DeleteAsync(string[] id)
        {
            return await DbContext.DeleteAsync<string, SysRolesModel>(id);
        }

        public async Task<long> DeleteAsync(SysRolesModel model)
        {
            return await DbContext.DeleteAsync(model);
        }

        public async Task<SysRolesModel> GetModelAsync(string id, IEnumerable<string> fields = null)
        {
            return await DbContext.GetModelAsync<string, SysRolesModel>(id,fields);
        }

        private string GetQuery(SysRolesRequestModel queryParameter, string table = null)
        {
            switch (table)
            {
                default:
                    {
                        var sb = new StringBuilder();
                        //如果需要模糊查询请处理参数值添加%
                        sb.Append(DbContext.GetSqlQueryString<SysRolesModel>(queryParameter));
                        return sb.ToString();
                    }
            }
        }

        public async Task<IEnumerable<SysRolesModel>> GetCurrentModelsAsync(SysRolesRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            using (var conn = DbContext.CreateConnection())
            {
                var strWhere = GetQuery(queryParameter);
            var strFieldNames = DbContext.GetFieldsToString<SysRolesModel>("b1", fields);
                var dataQuery = $"SELECT {strFieldNames} FROM {tableName} b1 WHERE 1=1 {strWhere} ORDER BY b1.role_id ASC";
                return await conn.QueryAsync<SysRolesModel>(dataQuery, queryParameter);
            }
        }

        public async Task<IEnumerable<SysRolesResponeModel>> GetModelsAsync(SysRolesRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            using (var conn = DbContext.CreateConnection())
            {
                var strWhere = GetQuery(queryParameter);
            var strFieldNames = DbContext.GetFieldsToString<SysRolesModel>("b1", fields);
                var dataQuery = $"SELECT b1_result.* FROM (SELECT {strFieldNames} FROM {tableName}  b1 WHERE 1=1 {strWhere}) b1_result ORDER BY b1_result.role_id ASC";//内查询，可以做连接查询 直接join
                return await conn.QueryAsync<SysRolesResponeModel>(dataQuery, queryParameter);
            }
        }

        public async Task<IEnumerable<SysRolesResponeModel>> GetModelsAsync(PagingModel pagingModel, SysRolesRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            var strWhere = GetQuery(queryParameter);
            var strFieldNames = DbContext.GetFieldsToString<SysRolesModel>("b1", fields);
            var querySql = "SELECT {0} " + $"FROM (SELECT {strFieldNames} FROM {tableName}  b1 WHERE 1=1 {strWhere} ORDER BY role_id ASC) b1_result";//内查询，可以做连接查询 直接join
            var countQuery = string.Format(querySql, "COUNT(1)");

            var pagingSql = $" LIMIT {pagingModel.StartIndex()},{pagingModel.PageSize}";//分页
            var dataQuery = string.Format(querySql, "b1_result.*") + pagingSql;

            using (var conn = DbContext.CreateConnection())
            {
                pagingModel.TotalCount = await conn.ExecuteScalarAsync<long>(countQuery,queryParameter);
                return await conn.QueryAsync<SysRolesResponeModel>(dataQuery, queryParameter); 
            }
        }

        public async Task<long> UpdateModelAsync(SysRolesModel model)
        {
            return await DbContext.UpdateModelAsync( model);
        }

        public async Task<long> UpdateAllModelAsync(SysRolesModel model)
        {
            return await DbContext.UpdateAllModelAsync(model);
        }
    }
}
