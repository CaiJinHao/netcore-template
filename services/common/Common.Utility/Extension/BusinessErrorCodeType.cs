using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Extension
{
    /// <summary>
    /// 业务错误编码类型
    /// </summary>
    public enum BusinessErrorCodeType
    {
        None=0,
        /// <summary>
        /// 致命异常，不要再执行了，直接被顶部处理直接返回
        /// 线程执行的中断线程
        /// 定时器执行的释放定时器
        /// </summary>
        FatalException = 1,
    }
}
