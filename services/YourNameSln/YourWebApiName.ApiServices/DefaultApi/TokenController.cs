using Common.JsonConverter;
using Common.NetCoreWebUtility.Services;
using Common.Utility.Extension;
using Common.Utility.Models;
using Common.Utility.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;
using YourWebApiName.IServices.IDbServices;

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
        /// <summary>
        /// 用户服务
        /// </summary>
        public ISysUsersService sysUsersService { get; set; }
        /// <summary>
        /// 获取TOKEN
        /// </summary>
        /// <param name="authModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] RequestAuthModel authModel)
        {
            var apiData = new ApiResultModel();
            var responesToken = await new ToKenService().GetTokenAsync(authModel);
            if (responesToken.IsError)
            {
                apiData.Code = ErrorCodeType.KeyOrSecretError;
                apiData.SetErrorCodeTypeMsg();
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
            var userId = UserHttpInfo.GetValueByToken(Common.Utility.Models.Config.TokenInfoType.UserId);
            var user =await sysUsersService.GetModelAsync(userId);//根据id获取用户信息
            var requestAuth = new RequestAuthModel()
            {
                Key = user.user_account,
                Secret = user.user_pwd
            };
            //通过用户信息获取最新的token
            var responesToken = await new ToKenService().GetTokenAsync(requestAuth);
            if (responesToken.IsError)
            {
                apiData.Code = ErrorCodeType.ServerError;
                apiData.SetErrorCodeTypeMsg();
            }
            else
            {
                apiData.Result = responesToken;
            }
            return Ok(apiData);
        }
    }
}