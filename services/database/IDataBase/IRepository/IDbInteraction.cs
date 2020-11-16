using IDataBase.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDataBase.IRepository
{
    /// <summary>
    /// 与数据库交互方法(基础) 由Repository继承
    /// TTableModel的名称必须和数据库表名称一致
    /// 继承者：所有IRepository,IDbContext
    /// </summary>
    public interface IDbInteraction<TTableModel, TResponeModel, TRequestModel, Tid, TPagingModel> : IDbServicesBase<TTableModel, TResponeModel, TRequestModel, Tid, TPagingModel>
    {
        /// <summary>
        /// 当前数据仓库的表名称
        /// </summary>
        string tableName { get; set; }

        /// <summary>
        /// 创建一个数组对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(TTableModel[] models);
    }
}
