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
    }
}
