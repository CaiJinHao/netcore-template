using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.IdentityServer.Models
{
    /// <summary>
    /// Claim变量名称配置
    /// </summary>
    public class ClaimConfigModel
    {
        /// <summary>
        /// 用于存储用户唯一标识
        /// </summary>
        public static string UserId = "user_id";
        /// <summary>
        /// 用于权限查询过滤
        /// </summary>
        public static string RoleId = "role_id";
        /// <summary>
        /// 用于存储用户角色名称，用来显示到UI端
        /// </summary>
        public static string RoleName = "role_name";
        /// <summary>
        /// 用户存储用户信息Json
        /// </summary>
        public static string UserInfo = "user_info";
    }
}
