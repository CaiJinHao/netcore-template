﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace IDataBase.ISql
{
    /// <summary>
    /// 关系型数据库方法
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
        /// 当需要更新老id为新的id的时候需要自己重新写sql,因为参数需要多一个参数
        /// 支持多主键条件更新
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="whereFields">更新条件字段</param>
        /// <returns></returns>
        Task<long> UpdateModelAsync<TTableModel>(TTableModel model, string[] whereFields = null, string[] notValidateFields = null) where TTableModel : class, new();
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
        /// <summary>
        /// 批量插入 暂时只支持SQLSERVER
        /// </summary>
        /// <typeparam name="TTableModel"></typeparam>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<long> CreateToBulk<TTableModel>(TTableModel[] models);
    }
}
