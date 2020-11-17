

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
    public class SysRoleMenuAndRepository : ISysRoleMenuAndRepository
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string tableName { get; set; }

       /// <summary>
       /// 资源 DbRepository 系统_角色菜单权限
       /// </summary>
       public SysRoleMenuAndRepository()
       {
            tableName = "sys_role_menu_and";
       }

        public IMySqlDbContext DbContext { get; set; }

        public async Task<bool> CreateAsync(SysRoleMenuAndModel model)
        {
          model.rma_time = DateTime.Now.Utc();
            return await DbContext.CreateAsync(model);
        }

        public async Task<bool> CreateAsync(SysRoleMenuAndModel[] models)
        {
            return await DbContext.CreateAsync(models);
        }

        public async Task<long> DeleteAsync(string[] id)
        {
            return await DbContext.DeleteAsync<string, SysRoleMenuAndModel>(id);
        }

        public async Task<long> DeleteAsync(SysRoleMenuAndModel model)
        {
            return await DbContext.DeleteAsync(model);
        }

        public async Task<SysRoleMenuAndModel> GetModelAsync(string id, IEnumerable<string> fields = null)
        {
            return await DbContext.GetModelAsync<string, SysRoleMenuAndModel>(id,fields);
        }

        private string GetQuery(SysRoleMenuAndRequestModel queryParameter, string table = null)
        {
            switch (table)
            {
                default:
                    {
                        var sb = new StringBuilder();
                        //如果需要模糊查询请处理参数值添加%
                        sb.Append(DbContext.GetSqlQueryString<SysRoleMenuAndModel>(queryParameter));
                        return sb.ToString();
                    }
            }
        }

        public async Task<IEnumerable<SysRoleMenuAndModel>> GetCurrentModelsAsync(SysRoleMenuAndRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            using (var conn = DbContext.CreateConnection())
            {
                var strWhere = GetQuery(queryParameter);
            var strFieldNames = DbContext.GetFieldsToString<SysRoleMenuAndModel>("b1", fields);
                var dataQuery = $"SELECT {strFieldNames} FROM {tableName} b1 WHERE 1=1 {strWhere} ORDER BY b1.rma_id ASC";
                return await conn.QueryAsync<SysRoleMenuAndModel>(dataQuery, queryParameter);
            }
        }
        /// <summary>
        /// 角色表可用的角色 内联接 不需要直接置为空
        /// </summary>
        private const string joinTable = "";
        /// <summary>
        /// 连接表需要查询字段 不需要直接置为空
        /// </summary>
        private const string joinFiles = "";

        public async Task<IEnumerable<SysRoleMenuAndResponeModel>> GetModelsAsync(SysRoleMenuAndRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            using (var conn = DbContext.CreateConnection())
            {
                var strWhere = GetQuery(queryParameter);
            var strFieldNames = DbContext.GetFieldsToString<SysRoleMenuAndModel>("b1", fields);
                var dataQuery = $"SELECT b1_result.*{joinFiles} FROM (SELECT {strFieldNames} FROM {tableName}  b1 WHERE 1=1 {strWhere}) b1_result {joinTable} ORDER BY b1_result.rma_id ASC";//内查询，可以做连接查询 直接join
                return await conn.QueryAsync<SysRoleMenuAndResponeModel>(dataQuery, queryParameter);
            }
        }

        public async Task<IEnumerable<SysRoleMenuAndResponeModel>> GetModelsAsync(PagingModel pagingModel, SysRoleMenuAndRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            var strWhere = GetQuery(queryParameter);
            var strFieldNames = DbContext.GetFieldsToString<SysRoleMenuAndModel>("b1", fields);
            var querySql = "SELECT {0} " + $"FROM (SELECT {strFieldNames} FROM {tableName}  b1 WHERE 1=1 {strWhere} ORDER BY rma_id ASC) b1_result {joinTable}";//内查询，可以做连接查询 直接join
            var countQuery = string.Format(querySql, "COUNT(1)");

            var pagingSql = $" LIMIT {pagingModel.StartIndex()},{pagingModel.PageSize}";//分页
            var dataQuery = string.Format(querySql, $"b1_result.*{joinFiles}") + pagingSql;

            using (var conn = DbContext.CreateConnection())
            {
                pagingModel.TotalCount = await conn.ExecuteScalarAsync<long>(countQuery,queryParameter);
                return await conn.QueryAsync<SysRoleMenuAndResponeModel>(dataQuery, queryParameter); 
            }
        }

        public async Task<long> UpdateModelAsync(SysRoleMenuAndModel model)
        {
            return await DbContext.UpdateModelAsync( model);
        }

        public async Task<long> UpdateAllModelAsync(SysRoleMenuAndModel model)
        {
            return await DbContext.UpdateAllModelAsync( model);
        }
    }
}
