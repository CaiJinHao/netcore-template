using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDataBase.IServices
{
    /// <summary>
    /// 逻辑层使用的接口、给控制器层调用的接口
    /// </summary>
    /// <typeparam name="TTableModel"></typeparam>
    /// <typeparam name="TResponeModel"></typeparam>
    /// <typeparam name="TRequestModel"></typeparam>
    /// <typeparam name="Tid"></typeparam>
    public interface IDbServicesBase<TTableModel, TResponeModel, TRequestModel, Tid, TPagingModel>
    {
        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(TTableModel model);
        /// <summary>
        /// 删除存在id的所有对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<long> DeleteAsync(Tid[] id);
        /// <summary>
        /// 根据主键更新对象中的指定属性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> UpdateModelAsync(Tid id, TTableModel model);
        /// <summary>
        /// 获取指定
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">结果字段集合</param>
        /// <returns></returns>
        Task<TTableModel> GetModelAsync(Tid id, IEnumerable<string> fields = null);
        /// <summary>
        /// 获取表中有条件的数据
        /// </summary>
        /// <param name="queryParameter"></param>
        /// <param name="fields">结果字段集合</param>
        /// <returns></returns>
        Task<IEnumerable<TResponeModel>> GetModelsAsync(TRequestModel queryParameter, IEnumerable<string> fields = null);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagingModel">分页器</param>
        /// <param name="queryParameter">查询对象</param>
        /// <param name="fields">结果字段集合</param>
        /// <returns></returns>
        Task<IEnumerable<TResponeModel>> GetModelsAsync(TPagingModel pagingModel, TRequestModel queryParameter, IEnumerable<string> fields = null);
    }
}
