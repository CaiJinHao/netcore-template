using DataBase.Redis.Common;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Redis
{
    public class RedisManager: IRedisManager
    {
        private readonly string redisConnenctionString;

        public volatile ConnectionMultiplexer redisConnection;

        private readonly object redisConnectionLock = new object();

        public RedisManager(string redisConfiguration)
        {
            if (string.IsNullOrWhiteSpace(redisConfiguration))
            {
                throw new ArgumentException("redis config is empty", nameof(redisConfiguration));
            }
            this.redisConnenctionString = redisConfiguration;
            this.redisConnection = GetRedisConnection();
        }

        /// <summary>
        /// 核心代码，获取连接实例
        /// 通过双if 夹lock的方式，实现单例模式
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetRedisConnection()
        {
            //如果已经连接实例，直接返回
            if (this.redisConnection != null && this.redisConnection.IsConnected)
            {
                return this.redisConnection;
            }
            //加锁，防止异步编程中，出现单例无效的问题
            lock (redisConnectionLock)
            {
                if (this.redisConnection != null)
                {
                    //释放redis连接
                    this.redisConnection.Dispose();
                }
                try
                {
                    var config = new ConfigurationOptions
                    {
                        AbortOnConnectFail = false,
                        AllowAdmin = true,
                        ConnectTimeout = 15000,//改成15s
                        SyncTimeout = 5000,
                        //Password = "Pwd",//Redis数据库密码
                        EndPoints = { redisConnenctionString }// connectionString 为IP:Port 如”192.168.2.110:6379”
                    };
                    this.redisConnection = ConnectionMultiplexer.Connect(config);
                }
                catch (Exception)
                {
                    throw new Exception("Redis服务未启用，本项目使用的的6379");
                }
            }
            return this.redisConnection;
        }
        /// <summary>
        /// 清除
        /// </summary>
        /// <returns>个别没有成功</returns>
        public async Task<bool> ClearAsync()
        {
            var rdata = true;
            foreach (var endPoint in this.GetRedisConnection().GetEndPoints())
            {
                var server = this.GetRedisConnection().GetServer(endPoint);
                foreach (var key in server.Keys())
                {
                    var _d = await redisConnection.GetDatabase().KeyDeleteAsync(key);
                    if (!_d)
                    {
                        rdata = _d;
                    }
                }
            }
            return rdata;
        }
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> GetAsync(string key)
        {
            return await redisConnection.GetDatabase().KeyExistsAsync(key);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetValueAsync(string key)
        {
            return await redisConnection.GetDatabase().StringGetAsync(key);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync<TEntity>(string key)
        {
            var value = await redisConnection.GetDatabase().StringGetAsync(key);
            if (value.HasValue)
            {
                //需要用的反序列化，将Redis存储的Byte[]，进行反序列化
                return Helper.Deserialize<TEntity>(value);
            }
            else
            {
                return default(TEntity);
            }
        }
        /// <summary>
        /// 根据模糊搜索获取多个
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="keysPattern"></param>
        /// <param name="dataBase"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetModelsAsync<TEntity>(string keysPattern, int dataBase = 0)
        {
            var rdata = new List<TEntity>();
            foreach (var endPoint in this.GetRedisConnection().GetEndPoints())
            {
                var server = this.GetRedisConnection().GetServer(endPoint);
                foreach (var key in server.Keys(dataBase, keysPattern))
                {
                    rdata.Add(await GetAsync<TEntity>(key));
                }
            }
            return rdata;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        public async Task RemoveAsync(string key)
        {
            await redisConnection.GetDatabase().KeyDeleteAsync(key);
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        public async Task SetAsync(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                //序列化，将object值生成RedisValue
                await redisConnection.GetDatabase().StringSetAsync(key, Helper.Serialize(value), cacheTime);
            }
        }

        /// <summary>
        /// 增加/修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> SetValueAsync(string key, byte[] value)
        {
            return await redisConnection.GetDatabase().StringSetAsync(key, value, TimeSpan.FromSeconds(120));
        }
    }
}
