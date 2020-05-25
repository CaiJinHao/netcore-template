using System.Threading.Tasks;

namespace IDataBase.INoSql
{
    /// <summary>
    /// 非关系型数据库上下文TODO:暂时没有用
    /// </summary>
    public interface INoSqlDbContext<TPrimaryKey> : IDbContextInteraction<TPrimaryKey>
    {
        /*
         U
         根据主键更新对象中的指定属性
         */
        /// <summary>
        /// 根据主键更新对象中的指定属性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model">只更新有值得，为null类型的不更新</param>
        /// <returns></returns>
        Task<long> UpdateModelAsync<Tid, TTableModel>(Tid id, TTableModel model, string notFieldRegex = "^_id") where TTableModel : class, new();
    }
}
