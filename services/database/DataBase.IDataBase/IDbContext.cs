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
    }
}
