using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.HttpModels
{
    /// <summary>
    /// 身份验证 获取TOKEN
    /// </summary>
    public  class AuthModel
    {
        /// <summary>
        /// KEY值
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 密匙
        /// </summary>
        public string Secret { get; set; }
    }
}
