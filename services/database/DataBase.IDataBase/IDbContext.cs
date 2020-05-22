using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.IDataBase
{
    /// <summary>
    /// 数据库上下文 连接
    /// 需要使用应用程序级生命周期单例Singleton
    /// </summary>
    public interface IDbContext<TDbConnection>
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        TDbConnection CreateConnection();

        /// <summary>
        /// 获取class对应的表明
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <returns></returns>
        string GetTableName<TTableModel>();
        /// <summary>
        /// 获取主键
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <returns></returns>
        string GetKeyName<TTableModel>();
        /// <summary>
        /// 获取table字段集合
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <param name="notInFields"></param>
        /// <returns></returns>
        IEnumerable<string> GetFields<TTableModel>(List<string> notInFields = null);
        /// <summary>
        /// 根据Model生成查询SQL
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        string GetSqlQueryString<TModel>(TModel model);
        /// <summary>
        /// 根据Model生成更新的SQL
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        string GetSqlUpdateString<TModel>(TModel model);
    }
}
