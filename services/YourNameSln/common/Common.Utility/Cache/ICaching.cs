using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Cache
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICaching
    {
        object Get(string cacheKey);

        void Set(string cacheKey, object cacheValue, double seconds=7200);
    }
}
