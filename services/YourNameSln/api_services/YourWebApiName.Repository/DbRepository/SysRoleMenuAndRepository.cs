

using YourWebApiName.IRepository.IDbRepository;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;
using Common.Utility.Extension;
using Common.Utility.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBse.MySqlFromDapper;
using System.Text;
using Dapper;

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

        public SysRoleMenuAndModel GetModel(string id)
        {
            return DbContext.GetModel<string, SysRoleMenuAndModel>(id);
        }

        public async Task<SysRoleMenuAndModel> GetModelAsync(string id)
        {
            return await DbContext.GetModelAsync<string, SysRoleMenuAndModel>(id);
        }

        public async Task<IEnumerable<SysRoleMenuAndModel>> GetModelsAsync()
        {
            return await DbContext.GetModelsAsync<SysRoleMenuAndModel>();
        }

        public Task<IEnumerable<dynamic>> GetNamesAsync(SysRoleMenuAndRequestModel queryParameter)
        {
            throw new NotImplementedException();
        }

        private string GetQuery(SysRoleMenuAndRequestModel queryParameter, Action<StringBuilder> appendSqlWhere)
        {
            var sqlWhere = new StringBuilder();//查询条件
            if (queryParameter.rma_id.IsNotNull())
            {
                sqlWhere.Append(" AND b1.rma_id = @rma_id");
            }
            if (queryParameter.menu_id.IsNotNull())
            {
                sqlWhere.Append(" AND b1.menu_id = @menu_id");
            }
            if (queryParameter.role_id.IsNotNull())
            {
                sqlWhere.Append(" AND b1.role_id = @role_id");
            }
            appendSqlWhere(sqlWhere);
            return sqlWhere.ToString();
        }

        public async Task<IEnumerable<SysRoleMenuAndModel>> GetCurrentModelsAsync(SysRoleMenuAndRequestModel queryParameter)
        {
            var strWhere = GetQuery(queryParameter, (sb) => { });
            var dataQuery = $"SELECT b1.* FROM {tableName} b1 WHERE 1=1 " + strWhere;
            return await DbContext.CreateConnection().QueryAsync<SysRoleMenuAndModel>(dataQuery, queryParameter);
        }

        public async Task<IEnumerable<SysRoleMenuAndResponeModel>> GetModelsAsync(SysRoleMenuAndRequestModel queryParameter)
        {
            var strWhere = GetQuery(queryParameter, (sb) => {
                //ViewModel 的条件过滤
                //if (!string.IsNullOrEmpty(queryParameter.xxx))
                //{
                //    sb.Append(" AND b2.xxx LIKE xxxx");
                //    queryObj.xxx += "%";
                //}
            });
            var dataQuery = $"SELECT b1.* FROM {tableName} b1 WHERE 1=1 " + strWhere;
            return await DbContext.CreateConnection().QueryAsync<SysRoleMenuAndResponeModel>(dataQuery, queryParameter);
        }

        public async Task<IEnumerable<SysRoleMenuAndResponeModel>> GetModelsAsync(PagingModel pagingModel, SysRoleMenuAndRequestModel queryParameter)
        {
            var strWhere = GetQuery(queryParameter, (sb) => {
                //ViewModel 的条件过滤
                //if (!string.IsNullOrEmpty(queryParameter.xxx))
                //{
                //    sb.Append(" AND b2.xxx LIKE xxxx");
                //    queryObj.xxx += "%";
                //}
            });
            var querySql = "SELECT {0} FROM "+ tableName + " b1 WHERE 1=1 " + strWhere;

            var countQuery = string.Format(querySql, "COUNT(1)");
            pagingModel.TotalCount = await DbContext.CreateConnection().ExecuteScalarAsync<long>(countQuery,queryParameter);

            var pagingSql = $" LIMIT {pagingModel.StartIndex()},{pagingModel.PageSize}";//分页
            var dataQuery = string.Format(querySql, "b1.*") + pagingSql;
            return await DbContext.GetModelsAsync<SysRoleMenuAndResponeModel, SysRoleMenuAndRequestModel>(dataQuery,queryParameter);
        }

        public async Task<long> UpdateModelAsync(string id, SysRoleMenuAndModel model)
        {
            return await DbContext.UpdateModelAsync(id, model);
        }

        public async Task<SysRoleMenuAndModel> GetFirstAsync(SysRoleMenuAndRequestModel queryParameter)
        {
            var tq = await GetModelsAsync(queryParameter);
            return tq.FirstOrDefault();
        }
    }
}
