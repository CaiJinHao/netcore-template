using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using Common.Utility.Models.Config;
using System;

namespace Common.Utility.Models.User
{
    /// <summary>
    /// 应用程序用户信息
    /// </summary>
    public class HttpInfo : IHttpInfo
    {
        public string GetUserId()
        {
            var claim= UserHttpContext.Current.User.Claims.Where(c=>c.Type== ClaimConfig.UserId).FirstOrDefault();
            if (claim==null)
            {
                throw new Exception("用户授权异常，获取不多claim信息");
            }
            return claim.Value;
        }


        public IEnumerable<Claim> GetClaimsIdentity()
        {
            //必须传header才能获取到数据
            return UserHttpContext.Current.User.Claims.ToList();
        }

        public string GetToken()
        {
            return UserHttpContext.Current.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        }

        public bool IsAuthenticated()
        {
            return UserHttpContext.Current.User.Identity.IsAuthenticated;
        }

        public string GetIp()
        {
            var ip = UserHttpContext.Current.Request.Headers["X-Forwarded-For"].ToString();
            if (string.IsNullOrEmpty(ip))
            {
                ip = UserHttpContext.Current.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}
