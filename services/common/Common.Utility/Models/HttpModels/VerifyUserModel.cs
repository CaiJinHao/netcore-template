using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.HttpModels
{
    /// <summary>
    /// 响应验证用户接口数据
    /// 跟GrantValidationResult一一对应
    /// </summary>
    public class VerifyUserModel
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string user_id { get; set; }
        public string role_id { get; set; }
        /// <summary>
        /// 用户角色名称
        /// </summary>
        public string role_name { get; set; }
        /// <summary>
        /// 当密码为md5的时候，需要知道账号密码
        /// 可以存成json吗？
        /// </summary>
        public string user_info { get; set; }
/*        /// <summary>
        /// 授权列表（可以是接口，可以是任何数组数据，这个数据会在header中可以获取到）
        /// </summary>
        public IEnumerable<string> auth_list { get; set; }
        /// <summary>
        /// 扩展其他数据
        /// </summary>
        public dynamic other_data { get; set; }*/
    }
}
