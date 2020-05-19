

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

        public SysMenusModel GetModel(string id)
        {
            return DbContext.GetModel<string, SysMenusModel>(id);
        }

        public async Task<SysMenusModel> GetModelAsync(string id)
        {
            return await DbContext.GetModelAsync<string, SysMenusModel>(id);
        }

        public async Task<IEnumerable<SysMenusModel>> GetModelsAsync()
        {
            return await DbContext.GetModelsAsync<SysMenusModel>();
        }

        public Task<IEnumerable<dynamic>> GetNamesAsync(SysMenusRequestModel queryParameter)
        {
            throw new NotImplementedException();
        }

        private string GetQuery(SysMenusRequestModel queryParameter, Action<StringBuilder> appendSqlWhere)
        {
            var sqlWhere = new StringBuilder();//查询条件
            if (queryParameter.menu_id.IsNotNull())
            {
                sqlWhere.Append(" AND b1.menu_id = @menu_id");
            }
            if (!string.IsNullOrEmpty(queryParameter.menu_name))
            {
                sqlWhere.Append(" AND b1.menu_name LIKE @menu_name");
                queryParameter.menu_name += "%";
            }
            if (!string.IsNullOrEmpty(queryParameter.menu_icon))
            {
                sqlWhere.Append(" AND b1.menu_icon LIKE @menu_icon");
                queryParameter.menu_icon += "%";
            }
            if (queryParameter.menu_sort>0)
            {
                sqlWhere.Append(" AND b1.menu_sort = @menu_sort");
            }
            if (queryParameter.menu_parent_id.IsNotNull())
            {
                sqlWhere.Append(" AND b1.menu_parent_id = @menu_parent_id");
            }
            if (queryParameter.menu_grade>0)
            {
                sqlWhere.Append(" AND b1.menu_grade = @menu_grade");
            }
            if (!string.IsNullOrEmpty(queryParameter.menu_url))
            {
                sqlWhere.Append(" AND b1.menu_url LIKE @menu_url");
                queryParameter.menu_url += "%";
            }
            if (!string.IsNullOrEmpty(queryParameter.menu_description))
            {
                sqlWhere.Append(" AND b1.menu_description LIKE @menu_description");
                queryParameter.menu_description += "%";
            }
            if (queryParameter.menu_is_enabled>0)
            {
                sqlWhere.Append(" AND b1.menu_is_enabled = @menu_is_enabled");
            }
            appendSqlWhere(sqlWhere);
            return sqlWhere.ToString();
        }

        public async Task<IEnumerable<SysMenusModel>> GetCurrentModelsAsync(SysMenusRequestModel queryParameter)
        {
            var strWhere = GetQuery(queryParameter, (sb) => { });
            var dataQuery = $"SELECT b1.* FROM {tableName} b1 WHERE 1=1 " + strWhere;
            return await DbContext.CreateConnection().QueryAsync<SysMenusModel>(dataQuery, queryParameter);
        }

        public async Task<IEnumerable<SysMenusResponeModel>> GetModelsAsync(SysMenusRequestModel queryParameter)
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
            return await DbContext.CreateConnection().QueryAsync<SysMenusResponeModel>(dataQuery, queryParameter);
        }

        public async Task<IEnumerable<SysMenusResponeModel>> GetModelsAsync(PagingModel pagingModel, SysMenusRequestModel queryParameter)
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
            return await DbContext.GetModelsAsync<SysMenusResponeModel, SysMenusRequestModel>(dataQuery,queryParameter);
        }

        public async Task<long> UpdateModelAsync(string id, SysMenusModel model)
        {
            return await DbContext.UpdateModelAsync(id, model);
        }

        public async Task<SysMenusModel> GetFirstAsync(SysMenusRequestModel queryParameter)
        {
            var tq = await GetModelsAsync(queryParameter);
            return tq.FirstOrDefault();
        }
    }
}
