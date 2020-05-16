using DataBase.IDataBase;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.MongoDb
{
    /// <summary>
    /// MongoDb数据上下文
    /// 如果需要再services层也拿到主键创建方法，这个对象也可直接放到services层里面
    /// </summary>
    /// <typeparam name="TDbConnection"></typeparam>
    /// <typeparam name="TTableModel"></typeparam>
    public interface IMongoDbContext : INoSqlDbContext<ObjectId>, IDbContext<IMongoDatabase>
    {
        /// <summary>
        /// 获取表 mongo
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <returns></returns>
        IMongoCollection<TTableModel> GetCollection<TTableModel>();
        /// <summary>
        /// 获取表 linq
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <returns></returns>
        IMongoQueryable<TTableModel> GetQueryable<TTableModel>();
    }
}
