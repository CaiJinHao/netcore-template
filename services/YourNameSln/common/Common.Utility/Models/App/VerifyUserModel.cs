using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.App
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
        public string UserId { get; set; }
        public string RoleId { get; set; }
        /// <summary>
        /// 用户角色名称
        /// </summary>
        public string RoleName { get; set; }
/*        /// <summary>
        /// 授权列表（可以是接口，可以是任何数组数据，这个数据会在header中可以获取到）
        /// </summary>
        public IEnumerable<string> AuthList { get; set; }
        /// <summary>
        /// 扩展其他数据
        /// </summary>
        public dynamic OtherData { get; set; }*/
    }
}
