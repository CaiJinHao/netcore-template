using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDataBase.ISql
{
    /// <summary>
    /// 关系型数据库方法
    /// TODO:能不能做到集成所有方法,这样方便更换数据库，继承该接口的针对性数据库解决方案，不能定义方法
    /// 如果写的SQL是针对数据库更换数据库时要更换Repository层
    /// </summary>
    public interface ISqlDbContext: IDbContextInteraction<string>
    {
        /// <summary>
        /// 获取指定
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TTableModel> GetModelAsync<Tid, TTableModel>(Tid id, IEnumerable<string> fields = null) where TTableModel : class, new();
        /// <summary>
        /// 根据主键更新对象中的指定属性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model">只更新有值得，为null类型的不更新</param>
        /// <returns></returns>
        Task<long> UpdateModelAsync<TTableModel>(TTableModel model, string[] notInFields = null) where TTableModel : class, new();
        /// <summary>
        /// 自增使用
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="notInFields">自增需要排除字段</param>
        /// <returns>返回自增值</returns>
        Task<long> CreateAsync<TTableModel>(TTableModel model, string[] notInFields) where TTableModel : class, new();

        /// <summary>
        /// 创建一个数组对象
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <param name="models"></param>
        /// <param name="notInFields">要排除的字段</param>
        /// <returns></returns>
        Task<bool> CreateAsync<TTableModel>(TTableModel[] models, string[] notInFields = null) where TTableModel : class, new();
    }
}
