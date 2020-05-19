

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

        public ISqlServerDbContext DbContext { get; set; }

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

        public SysUsersModel GetModel(string id)
        {
            return DbContext.GetModel<string, SysUsersModel>(id);
        }

        public async Task<SysUsersModel> GetModelAsync(string id)
        {
            return await DbContext.GetModelAsync<string, SysUsersModel>(id);
        }

        public async Task<IEnumerable<SysUsersModel>> GetModelsAsync()
        {
            return await DbContext.GetModelsAsync<SysUsersModel>();
        }

        public Task<IEnumerable<dynamic>> GetNamesAsync(SysUsersRequestModel queryParameter)
        {
            throw new NotImplementedException();
        }

        private string GetQuery(SysUsersRequestModel queryParameter, string table = null)
        {

            switch (table)
            {
                default:
                    {
                        var sqlWhere = new StringBuilder();//查询条件
                        if (queryParameter.user_id.IsNotNull())
                        {
                            sqlWhere.Append(" AND b1.user_id = @user_id");
                        }
                        if (queryParameter.role_id.IsNotNull())
                        {
                            sqlWhere.Append(" AND b1.role_id = @role_id");
                        }
                        if (!string.IsNullOrEmpty(queryParameter.user_account))
                        {
                            sqlWhere.Append(" AND b1.user_account LIKE @user_account");
                            queryParameter.user_account += "%";
                        }
                        if (!string.IsNullOrEmpty(queryParameter.user_pwd))
                        {
                            sqlWhere.Append(" AND b1.user_pwd LIKE @user_pwd");
                            queryParameter.user_pwd += "%";
                        }
                        if (queryParameter.user_is_enable > 0)
                        {
                            sqlWhere.Append(" AND b1.user_is_enable = @user_is_enable");
                        }
                        if (!string.IsNullOrEmpty(queryParameter.user_phone))
                        {
                            sqlWhere.Append(" AND b1.user_phone LIKE @user_phone");
                            queryParameter.user_phone += "%";
                        }
                        if (!string.IsNullOrEmpty(queryParameter.user_email))
                        {
                            sqlWhere.Append(" AND b1.user_email LIKE @user_email");
                            queryParameter.user_email += "%";
                        }
                        if (!string.IsNullOrEmpty(queryParameter.user_icon))
                        {
                            sqlWhere.Append(" AND b1.user_icon LIKE @user_icon");
                            queryParameter.user_icon += "%";
                        }
                        if (!string.IsNullOrEmpty(queryParameter.user_name))
                        {
                            sqlWhere.Append(" AND b1.user_name LIKE @user_name");
                            queryParameter.user_name += "%";
                        }
                        if (queryParameter.user_sex > 0)
                        {
                            sqlWhere.Append(" AND b1.user_sex = @user_sex");
                        }
                        return sqlWhere.ToString();
                    }
            }
        }

        public async Task<IEnumerable<SysUsersModel>> GetCurrentModelsAsync(SysUsersRequestModel queryParameter)
        {
            var strWhere = GetQuery(queryParameter);
            var dataQuery = $"SELECT b1.* FROM {tableName} b1 WHERE 1=1 " + strWhere;
            return await DbContext.CreateConnection().QueryAsync<SysUsersModel>(dataQuery, queryParameter);
        }

        public async Task<IEnumerable<SysUsersResponeModel>> GetModelsAsync(SysUsersRequestModel queryParameter)
        {
            var strWhere = GetQuery(queryParameter);
            var dataQuery = $"SELECT b1_result.* FROM (SELECT b1.* FROM {tableName}  b1 WHERE 1=1 {strWhere}) b1_result";//内查询，可以做连接查询 直接join
            return await DbContext.CreateConnection().QueryAsync<SysUsersResponeModel>(dataQuery, queryParameter);
        }

        public async Task<IEnumerable<SysUsersResponeModel>> GetModelsAsync(PagingModel pagingModel, SysUsersRequestModel queryParameter)
        {
            var strWhere = GetQuery(queryParameter);
            var querySql = "SELECT {0} " + $"FROM (SELECT b1.* FROM {tableName}  b1 WHERE 1=1 {strWhere}) b1_result";//内查询，可以做连接查询 直接join
            var countQuery = string.Format(querySql, "COUNT(1)");
            pagingModel.TotalCount = await DbContext.CreateConnection().ExecuteScalarAsync<long>(countQuery, queryParameter);

            var pagingQuerySql = string.Format(querySql, "ROW_NUMBER() OVER(ORDER BY user_id ASC) AS RowNum,b1_result.*");//按带索引的字段排序，否则很慢
            var dataQuery = $"SELECT * FROM ({pagingQuerySql}) tdata WHERE tdata.RowNum BETWEEN {pagingModel.StartIndex()} and {pagingModel.PageSize * pagingModel.Page}";
            return await DbContext.GetModelsAsync<SysUsersResponeModel, SysUsersRequestModel>(dataQuery, queryParameter);
        }

        public async Task<long> UpdateModelAsync(string id, SysUsersModel model)
        {
            return await DbContext.UpdateModelAsync(id, model);
        }

        public async Task<SysUsersModel> GetFirstAsync(SysUsersRequestModel queryParameter)
        {
            var tq = await GetModelsAsync(queryParameter);
            return tq.FirstOrDefault();
        }
    }
}
