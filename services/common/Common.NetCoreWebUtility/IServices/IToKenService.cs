using Common.Utility.RequestModels;
using Common.Utility.ResponesModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.NetCoreWebUtility.IServices
{
    public interface IToKenService
    {
        /// <summary>
        /// 获取TOKEN
        /// </summary>
        /// <returns></returns>
        Task<ResponesToKenModel> GetTokenAsync(RequestAuthModel requestAuthModel);
        /// <summary>
        /// 获取用户唯一标识
        /// </summary>
        /// <returns></returns>
        string GetUserId();
        /// <summary>
        /// 获取用户角色Id
        /// </summary>
        /// <returns></returns>
        string GetRoleId();
    }
}
