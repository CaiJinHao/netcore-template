﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDataBase
{
    /// <summary>
    /// 数据库上下文基础操作,由IDbContext继承
    /// 所有数据库方法：NoSql(非关系型代表)/Sql(关系型代表)
    /// </summary>
    public interface IDbContextInteraction<TPrimaryKey>
    {
        /*
         CRUD
         TTableModel 数据库映射对象实体
         Tid	主键类型
         
        幂等性：GET/DELETE/PUT天然幂等HTTP请求
        非幂等：POST(或者不做处理)
        1.一个TOKEN只能请求一次
             */

        TPrimaryKey PrimaryKey { get;}

        /*
         Create 
         创建一个对象
         创建一个数组对象
             */
        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> CreateAsync<TTableModel>(TTableModel model) where TTableModel : class, new();
      

        /*
         Delete 
         删除存在id的所有对象
             */
        /// <summary>
        /// 删除存在id的所有对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<long> DeleteAsync<Tid, TTableModel>(Tid[] id) where TTableModel : class, new();
        /// <summary>
        /// 根据条件删除多条数据
        /// </summary>
        /// <typeparam name="Tid"></typeparam>
        /// <typeparam name="TTableModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> DeleteAsync<TTableModel>(TTableModel model, string[] notInFields = null) where TTableModel : class, new();
    }
}
