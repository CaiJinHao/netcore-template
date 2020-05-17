using Common.Utility.Models.App;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.IDataBase
{
    /// <summary>
    /// 与数据库交互方法(基础) 由Service和Repository同时继承
    /// TTableModel的名称必须和数据库表名称一致
    /// 继承者：所有IRepository,IDbContext
    /// </summary>
    public interface IDbInteraction<TTableModel, TResponeModel, TRequestModel, Tid>: IDbServicesBase<TTableModel, TResponeModel, TRequestModel, Tid>
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

        /*
         Retrieve 读取
         获取指定
         获取表中所有数据
         获取表中所有数据,根据多条件查询过滤
         获取表中所有数据,根据多条件查询过滤,并且分页处理
         获取表中所有数据总数量
             */
        /// <summary>
        /// 同步调用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TTableModel GetModel(Tid id);
        /// <summary>
        /// 获取表中所有数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TTableModel>> GetModelsAsync();
        /// <summary>
        /// 获取信息名称数据供下拉列表使用
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetNamesAsync(TRequestModel queryParameter);
        /// <summary>
        /// 获取条件的第一个
        /// </summary>
        /// <param name="queryParameter"></param>
        /// <returns></returns>
        Task<TTableModel> GetFirstAsync(TRequestModel queryParameter);
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
