using Common.Utility.Models.Config;
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
        /// 获取信息
        /// </summary>
        /// <param name="tokenInfoType"></param>
        /// <returns></returns>
        string GetValueByToken(TokenInfoType tokenInfoType);
    }
}
