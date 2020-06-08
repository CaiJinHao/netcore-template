using System.Collections.Generic;

namespace IDataBase
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
        IEnumerable<string> GetFields<TTableModel>(string[] notInFields = null);
        /// <summary>
        /// 获取用","号隔开的字段
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <param name="fieldTableName">字段表别名前缀</param>
        /// <param name="fields">结果字段集合</param>
        /// <returns></returns>
        string GetFieldsToString<TTableModel>(string fieldTableName, IEnumerable<string> fields = null);
        /// <summary>
        /// 根据Model生成查询SQL
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        string GetSqlQueryString<TModel>(TModel model, string[] notInFields = null, string fieldPrefix = "b1.");
        /// <summary>
        /// 根据Model生成更新的SQL
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        string GetSqlUpdateString<TModel>(TModel model, string[] notInFields = null);
    }
}
