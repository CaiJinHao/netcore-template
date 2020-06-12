using Common.JsonConverter;
using Common.NetCoreWebUtility.Services;
using Common.Utility.Extension;
using Common.Utility.Models;
using Common.Utility.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

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
        /// Token
        /// </summary>
        public ToKenService toKenService { get; set; }
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
            var userJson = UserHttpInfo.GetValueByToken(Common.Utility.Models.Config.TokenInfoType.UserInfo);//这个应该封装到UserService中
            var user = JsonSerializer.Deserialize<dynamic>(userJson, new JsonSerializerOptions() { Converters = { new DynamicJsonConverter() } });
            var requestAuth = new RequestAuthModel()
            {
                Key = (string)user.key,
                Secret = (string)user.secret
            };
            //通过用户信息获取最新的token
            var responesToken = await toKenService.GetTokenAsync(requestAuth);
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