

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
    public class SysMenusRepository : ISysMenusRepository
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string tableName { get; set; }

       /// <summary>
       /// 资源 DbRepository 系统_菜单
       /// </summary>
       public SysMenusRepository()
       {
            tableName = "sys_menus";
       }

        public IMySqlDbContext DbContext { get; set; }

        public async Task<bool> CreateAsync(SysMenusModel model)
        {
            return await DbContext.CreateAsync(model);
        }

        public async Task<bool> CreateAsync(SysMenusModel[] models)
        {
            return await DbContext.CreateAsync(models);
        }

        public async Task<long> DeleteAsync(string[] id)
        {
            return await DbContext.DeleteAsync<string, SysMenusModel>(id);
        }

        public async Task<long> DeleteAsync(SysMenusModel model)
        {
            return await DbContext.DeleteAsync(model);
        }

        public async Task<SysMenusModel> GetModelAsync(string id, IEnumerable<string> fields = null)
        {
            return await DbContext.GetModelAsync<string, SysMenusModel>(id,fields);
        }

        private string GetQuery(SysMenusRequestModel queryParameter, string table = null)
        {
            switch (table)
            {
                default:
                    {
                        var sb = new StringBuilder();
                        //如果需要模糊查询请处理参数值添加%
                        sb.Append(DbContext.GetSqlQueryString<SysMenusModel>(queryParameter));
                        return sb.ToString();
                    }
            }
        }

        public async Task<IEnumerable<SysMenusModel>> GetCurrentModelsAsync(SysMenusRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            using (var conn = DbContext.CreateConnection())
            {
                var strWhere = GetQuery(queryParameter);
            var strFieldNames = DbContext.GetFieldsToString<SysMenusModel>("b1", fields);
                var dataQuery = $"SELECT {strFieldNames} FROM {tableName} b1 WHERE 1=1 {strWhere} ORDER BY b1.menu_id ASC";
                return await conn.QueryAsync<SysMenusModel>(dataQuery, queryParameter);
            }
        }

        public async Task<IEnumerable<SysMenusResponeModel>> GetModelsAsync(SysMenusRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            using (var conn = DbContext.CreateConnection())
            {
                var strWhere = GetQuery(queryParameter);
            var strFieldNames = DbContext.GetFieldsToString<SysMenusModel>("b1", fields);
                var dataQuery = $"SELECT b1_result.* FROM (SELECT {strFieldNames} FROM {tableName}  b1 WHERE 1=1 {strWhere}) b1_result ORDER BY b1_result.menu_id ASC";//内查询，可以做连接查询 直接join
                return await conn.QueryAsync<SysMenusResponeModel>(dataQuery, queryParameter);
            }
        }

        public async Task<IEnumerable<SysMenusResponeModel>> GetModelsAsync(PagingModel pagingModel, SysMenusRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            var strWhere = GetQuery(queryParameter);
            var strFieldNames = DbContext.GetFieldsToString<SysMenusModel>("b1", fields);
            var querySql = "SELECT {0} " + $"FROM (SELECT {strFieldNames} FROM {tableName}  b1 WHERE 1=1 {strWhere} ORDER BY menu_id ASC) b1_result";//内查询，可以做连接查询 直接join
            var countQuery = string.Format(querySql, "COUNT(1)");

            var pagingSql = $" LIMIT {pagingModel.StartIndex()},{pagingModel.PageSize}";//分页
            var dataQuery = string.Format(querySql, "b1_result.*") + pagingSql;

            using (var conn = DbContext.CreateConnection())
            {
                pagingModel.TotalCount = await conn.ExecuteScalarAsync<long>(countQuery,queryParameter);
                return await conn.QueryAsync<SysMenusResponeModel>(dataQuery, queryParameter); 
            }
        }

        public async Task<long> UpdateModelAsync(SysMenusModel model)
        {
            return await DbContext.UpdateModelAsync( model);
        }

        public async Task<long> UpdateAllModelAsync(SysMenusModel model)
        {
            return await DbContext.UpdateAllModelAsync( model);
        }
    }
}
