using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Common.Utility.Models.User
{
    public interface IHttpInfo
    {
        /// <summary>
        /// 获取用户唯一标识
        /// </summary>
        /// <returns></returns>
        string GetUserId();
        /// <summary>
        /// 获取客户IP
        /// </summary>
        /// <returns></returns>
        string GetIp();


        string GetToken();
        IEnumerable<Claim> GetClaimsIdentity();
        bool IsAuthenticated();
    }
}
