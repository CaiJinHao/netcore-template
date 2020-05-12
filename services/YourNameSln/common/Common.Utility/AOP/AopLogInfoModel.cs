using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.AOP
{
    /// <summary>
    /// 拦截器日志信息实体
    /// </summary>
    public class AopLogInfoModel
    {
        /// <summary>
        /// 代理接口
        /// </summary>
        public string ProxyInterface { get; set; }
        /// <summary>
        /// 调用服务
        /// </summary>
        public string InvocationService { get; set; }
        /// <summary>
        /// 执行方法
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 携带参数
        /// </summary>
        public string Arguments { get; set; }
        /// <summary>
        /// 返回类型
        /// </summary>
        public string ReturnType { get; set; }
        /// <summary>
        /// 返回值
        /// </summary>
        public string ReturnValue { get; set; }
    }
}
