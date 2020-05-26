﻿using Common.Utility.Encryption;
using Common.Utility.Models;
using Common.Utility.Models.HttpModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace YourWebApiName.ApiServices.DefaultApi
{
    /// <summary>
    /// 验证用户
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]//允许匿名访问
    public class VerifyUserController : ControllerBase
    {
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="key">账号</param>
        /// <param name="secret">密码</param>
        /// <returns></returns>
        [HttpGet("{key}/{secret}")]
        public async Task<IActionResult> Get(string key, string secret)
        {
            var apiData = new ApiResultModel();
            var rvu_data = await VerifyUser(key, secret);
            if (rvu_data == null)
            {
                apiData.Code = ErrorCodeType.KeyOrSecretError;
                return Ok(apiData);
            }
            apiData.Result = rvu_data;
            return Ok(apiData);
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="key">账号</param>
        /// <param name="secret">密码</param>
        /// <returns></returns>
        private async Task<VerifyUserModel> VerifyUser(string key, string secret)
        {
            await Task.Delay(1);
            var pwd = key + secret;
            pwd = EncrypHelper.EncryptToMD5(pwd);
            var user = new { user_id = "woshiuserid", role_name = "角色名称" };
            if (user == null)
            {
                //验证不通过
                return null;
            }

            var tokenUserJson = JsonSerializer.Serialize(new { key, secret });
            return new VerifyUserModel()
            {
                user_id = user.user_id,
                role_id = "none",
                role_name = user.role_name,
                user_info = tokenUserJson
            };
        }
    }
}