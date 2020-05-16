﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.User
{
    /// <summary>
    /// 用户 HttpContext
    /// </summary>
    public static class UserHttpContext
    {
        private static IHttpContextAccessor _accessor;
        /// <summary>
        /// 必须直接用否则可能会获取不到
        /// </summary>
        public static HttpContext Current => _accessor?.HttpContext;

        /// <summary>
        /// 如果不用这个可以参考 AppUser实现
        /// </summary>
        /// <param name="accessor"></param>
        public static void Configure(IHttpContextAccessor accessor)
        { 
            _accessor = accessor;
        }
    }
}
