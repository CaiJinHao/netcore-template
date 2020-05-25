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
        /// 根据主键更新对象中的指定属性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model">只更新有值得，为null类型的不更新</param>
        /// <returns></returns>
        Task<long> UpdateModelAsync<Tid, TTableModel>(Tid id, TTableModel model) where TTableModel : class, new();
    }
}
