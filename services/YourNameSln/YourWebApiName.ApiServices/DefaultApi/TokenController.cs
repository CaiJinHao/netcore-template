using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.NetCoreWebUtility.IServices;
using Common.Utility.Models;
using Common.Utility.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YourWebApiName.ApiServices.DefaultApi
{
    /// <summary>
    /// 身份验证
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IToKenService toKenService { get; set; }
        /// <summary>
        /// 获取TOKEN
        /// </summary>
        /// <param name="authModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]RequestAuthModel authModel)
        {
            var apiData = new ApiResultModel();
            var responesToken = await toKenService.GetTokenAsync(authModel);
            if (!string.IsNullOrEmpty(responesToken.Error))
            {
                apiData.Code = ErrorCodeType.KeyOrSecretError;
                apiData.Msg = responesToken.Error;
            }
            else
            {
                apiData.Result = responesToken;
            }
            return Ok(apiData);
        }

        /// <summary>
        /// 拿旧TOKEN刷新TOKEN
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var apiData = new ApiResultModel();
            var user_id = toKenService.GetUserId();
            //通过userid获取用户信息
            var user = new { user_id = user_id, pwd = "woshiroleid456" };
            //通过用户信息获取最新的token
            var responesToken = await toKenService.GetTokenAsync(new RequestAuthModel()
            {
                IotKey = user.user_id,
                IotSecret = user.pwd
            });
            if (!string.IsNullOrEmpty(responesToken.Error))
            {
                apiData.Code = ErrorCodeType.ServerError;
                apiData.Msg = responesToken.Error;
            }
            else
            {
                apiData.Result = responesToken;
            }
            return Ok(apiData);
        }
    }
}