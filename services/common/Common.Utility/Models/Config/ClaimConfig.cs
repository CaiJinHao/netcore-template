﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config
{
    /*
     值必须小写，否则添加不到token中
     必须配置ApiResource 的ClaimTypes 否则不会携带
     请确保和IdentityServer中保持一致
         */

    /// <summary>
    /// Claim变量名称配置
    /// </summary>
    public class ClaimConfig
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

    /// <summary>
    /// TokenInfoType
    /// </summary>
    public enum TokenInfoType
    {
        UserId=1,
        RoleId=2,
        RoleName=3,
        UserInfo=4
    }
}
