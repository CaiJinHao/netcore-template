using System;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Cache
{
    /// <summary>
    /// 实例化缓存接口ICaching
    /// </summary>
    public class MemoryCaching : ICaching
    {
        private readonly IMemoryCache _cache;

        public MemoryCaching(IMemoryCache cache)
        {
            _cache = cache;
        }

        public object Get(string cacheKey)
        {
            return _cache.Get(cacheKey);
        }

        public void Set(string cacheKey, object cacheValue, double seconds = 7200)
        {
            _cache.Set(cacheKey, cacheValue, TimeSpan.FromSeconds(seconds));
        }
    }
}
