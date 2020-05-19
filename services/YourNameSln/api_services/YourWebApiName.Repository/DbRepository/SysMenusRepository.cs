

using Common.Utility.Extension;
using Common.Utility.Models.App;
using Dapper;
using DataBase.DapperForSqlServer;
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

        public ISqlServerDbContext DbContext { get; set; }

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

        private string GetQuery(SysMenusRequestModel queryParameter, string table = null)
        {
          switch (table)
            {
                default:
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
            return sqlWhere.ToString();
     }
            }
        }

        public async Task<IEnumerable<SysMenusModel>> GetCurrentModelsAsync(SysMenusRequestModel queryParameter)
        {
            var strWhere = GetQuery(queryParameter);
            var dataQuery = $"SELECT b1.* FROM {tableName} b1 WHERE 1=1 {strWhere} ORDER BY b1.menu_id ASC";
            return await DbContext.CreateConnection().QueryAsync<SysMenusModel>(dataQuery, queryParameter);
        }

        public async Task<IEnumerable<SysMenusResponeModel>> GetModelsAsync(SysMenusRequestModel queryParameter)
        {
            var strWhere = GetQuery(queryParameter);
            var dataQuery = $"SELECT b1_result.* FROM (SELECT b1.* FROM {tableName}  b1 WHERE 1=1 {strWhere}) b1_result ORDER BY b1_result.menu_id ASC";//内查询，可以做连接查询 直接join
            return await DbContext.CreateConnection().QueryAsync<SysMenusResponeModel>(dataQuery, queryParameter);
        }

        public async Task<IEnumerable<SysMenusResponeModel>> GetModelsAsync(PagingModel pagingModel, SysMenusRequestModel queryParameter)
        {
            var strWhere = GetQuery(queryParameter);
            var querySql = "SELECT {0} " + $"FROM (SELECT b1.* FROM {tableName}  b1 WHERE 1=1 {strWhere}) b1_result";//内查询，可以做连接查询 直接join
            var countQuery = string.Format(querySql, "COUNT(1)");
            pagingModel.TotalCount = await DbContext.CreateConnection().ExecuteScalarAsync<long>(countQuery,queryParameter);

            var pagingQuerySql = string.Format(querySql, "ROW_NUMBER() OVER(ORDER BY b1_result.menu_id ASC) AS RowNum,b1_result.*");//按带索引的字段排序，否则很慢
            var dataQuery = $"SELECT * FROM ({pagingQuerySql}) tdata WHERE tdata.RowNum BETWEEN {pagingModel.StartIndex()} and {pagingModel.PageSize * pagingModel.Page}";
            return await DbContext.GetModelsAsync<SysMenusResponeModel, SysMenusRequestModel>(dataQuery, queryParameter);
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
