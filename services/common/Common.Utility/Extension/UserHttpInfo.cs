using Common.Utility.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Common.Utility.Extension
{
    /// <summary>
    /// 应用程序用户信息
    /// </summary>
    public class UserHttpInfo: UserHttpContext
    {
        public static IEnumerable<Claim> GetClaimsIdentity()
        {
            //必须传header才能获取到数据
            return Current.User.Claims.ToList();
        }

        public static string GetToken()
        {
            return Current.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        }

        public static bool IsAuthenticated()
        {
            return Current.User.Identity.IsAuthenticated;
        }

        public static string GetIp()
        {
            var ip = Current.Request.Headers["X-Forwarded-For"].ToString();
            if (string.IsNullOrEmpty(ip))
            {
                ip = Current.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

        /// <summary>
        /// 根据类型获取token中的信息
        /// </summary>
        /// <param name="tokenInfoType"></param>
        /// <returns></returns>
        public static string GetValueByToken(TokenInfoType tokenInfoType)
        {
            string infoType = string.Empty;
            switch (tokenInfoType)
            {
                case TokenInfoType.UserId:
                    infoType = ClaimConfig.UserId;
                    break;
                case TokenInfoType.RoleId:
                    infoType = ClaimConfig.RoleId;
                    break;
                case TokenInfoType.RoleName:
                    infoType = ClaimConfig.RoleName;
                    break;
                case TokenInfoType.UserInfo:
                    infoType = ClaimConfig.UserInfo;
                    break;
                default:
                    break;
            }
            var claim = Current.User.Claims.Where(c => c.Type == infoType).FirstOrDefault();
            if (claim == null)
            {
                throw new Exception("用户授权异常，获取不多Token内容信息");
            }
            return claim.Value;
        }
    }
}
