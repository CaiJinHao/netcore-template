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
        /*
         Create 
         创建一个对象
         创建一个数组对象
             */
       
        /// <summary>
        /// 创建一个数组对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(TTableModel[] models);

/*        /// <summary>
        /// 获取信息名称数据供下拉列表使用
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetNamesAsync(TRequestModel queryParameter);*/
        /// <summary>
        /// 获取当前model表
        /// </summary>
        /// <param name="queryParameter"></param>
        /// <returns></returns>
        Task<IEnumerable<TTableModel>> GetCurrentModelsAsync(TRequestModel queryParameter);


        /*
         U
         根据主键更新对象中的指定属性
         */
        


        /*
         Delete 
         删除存在id的所有对象
             */
        
        /// <summary>
        /// 根据条件删除多条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> DeleteAsync(TTableModel model);
    }
}
