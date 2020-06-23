using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Redis
{
    public interface IRedisManager
    {
        /// <summary>
        /// 获取 Reids 缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetValueAsync(string key);

        /// <summary>
        /// 获取值，并序列化
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync<TEntity>(string key);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        Task SetAsync(string key, object value, TimeSpan cacheTime);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> GetAsync(string key);

        /// <summary>
        /// 移除某一个缓存值
        /// </summary>
        /// <param name="key"></param>
        Task RemoveAsync(string key);

        /// <summary>
        /// 全部清除
        /// </summary>
        Task<bool> ClearAsync();

        /// <summary>
        /// 模糊匹配多个
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="keysPattern"></param>
        /// <param name="dataBase"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetModelsAsync<TEntity>(string keysPattern, int dataBase = 0);
    }
}
