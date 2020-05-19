

using Common.Utility.Extension;
using Common.Utility.Models.App;
using Dapper;
using DataBase.DapperForMySql;
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

        public SysRolesModel GetModel(string id)
        {
            return DbContext.GetModel<string, SysRolesModel>(id);
        }

        public async Task<SysRolesModel> GetModelAsync(string id)
        {
            return await DbContext.GetModelAsync<string, SysRolesModel>(id);
        }

        public async Task<IEnumerable<SysRolesModel>> GetModelsAsync()
        {
            return await DbContext.GetModelsAsync<SysRolesModel>();
        }

        public Task<IEnumerable<dynamic>> GetNamesAsync(SysRolesRequestModel queryParameter)
        {
            throw new NotImplementedException();
        }

        private string GetQuery(SysRolesRequestModel queryParameter, string table = null)
        {
            switch (table)
            {
                default:
                    {
                        var sqlWhere = new StringBuilder();//查询条件
                        if (queryParameter.role_id.IsNotNull())
                        {
                            sqlWhere.Append(" AND b1.role_id = @role_id");
                        }
                        if (!string.IsNullOrEmpty(queryParameter.role_name))
                        {
                            sqlWhere.Append(" AND b1.role_name LIKE @role_name");
                            queryParameter.role_name += "%";
                        }
                        if (!string.IsNullOrEmpty(queryParameter.role_remarks))
                        {
                            sqlWhere.Append(" AND b1.role_remarks LIKE @role_remarks");
                            queryParameter.role_remarks += "%";
                        }
                        if (!string.IsNullOrEmpty(queryParameter.role_parent_role))
                        {
                            sqlWhere.Append(" AND b1.role_parent_role LIKE @role_parent_role");
                            queryParameter.role_parent_role += "%";
                        }
                        if (queryParameter.role_sort > 0)
                        {
                            sqlWhere.Append(" AND b1.role_sort = @role_sort");
                        }
                        if (queryParameter.role_grade > 0)
                        {
                            sqlWhere.Append(" AND b1.role_grade = @role_grade");
                        }
                        if (queryParameter.role_is_enable > 0)
                        {
                            sqlWhere.Append(" AND b1.role_is_enable = @role_is_enable");
                        }
                        return sqlWhere.ToString();
                    }
            }
        }

        public async Task<IEnumerable<SysRolesModel>> GetCurrentModelsAsync(SysRolesRequestModel queryParameter)
        {
            var strWhere = GetQuery(queryParameter);
            var dataQuery = $"SELECT b1.* FROM {tableName} b1 WHERE 1=1 {strWhere} ORDER BY role_id ASC";
            return await DbContext.CreateConnection().QueryAsync<SysRolesModel>(dataQuery, queryParameter);
        }

        public async Task<IEnumerable<SysRolesResponeModel>> GetModelsAsync(SysRolesRequestModel queryParameter)
        {
            var strWhere = GetQuery(queryParameter);
            var dataQuery = $"SELECT b1_result.* FROM (SELECT b1.* FROM {tableName}  b1 WHERE 1=1 {strWhere} ORDER BY role_id ASC) b1_result";//内查询，可以做连接查询 直接join
            return await DbContext.CreateConnection().QueryAsync<SysRolesResponeModel>(dataQuery, queryParameter);
        }

        public async Task<IEnumerable<SysRolesResponeModel>> GetModelsAsync(PagingModel pagingModel, SysRolesRequestModel queryParameter)
        {
            var strWhere = GetQuery(queryParameter);
            var querySql = "SELECT {0} " + $"FROM (SELECT b1.* FROM {tableName}  b1 WHERE 1=1 {strWhere} ORDER BY role_id ASC) b1_result";//内查询，可以做连接查询 直接join
            var countQuery = string.Format(querySql, "COUNT(1)");
            pagingModel.TotalCount = await DbContext.CreateConnection().ExecuteScalarAsync<long>(countQuery, queryParameter);

            var pagingSql = $" LIMIT {pagingModel.StartIndex()},{pagingModel.PageSize}";//分页
            var dataQuery = string.Format(querySql, "b1_result.*") + pagingSql;
            return await DbContext.GetModelsAsync<SysRolesResponeModel, SysRolesRequestModel>(dataQuery, queryParameter);
        }

        public async Task<long> UpdateModelAsync(string id, SysRolesModel model)
        {
            return await DbContext.UpdateModelAsync(id, model);
        }

        public async Task<SysRolesModel> GetFirstAsync(SysRolesRequestModel queryParameter)
        {
            var tq = await GetModelsAsync(queryParameter);
            return tq.FirstOrDefault();
        }
    }
}
