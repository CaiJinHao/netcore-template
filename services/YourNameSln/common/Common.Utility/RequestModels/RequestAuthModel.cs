using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.RequestModels
{
    /// <summary>
    /// 身份验证 获取TOKEN
    /// </summary>
    public class RequestAuthModel
    {
        /// <summary>
        /// KEY值
        /// </summary>
        public string IotKey { get; set; }
        /// <summary>
        /// 密匙
        /// </summary>
        public string IotSecret { get; set; }
    }
}
