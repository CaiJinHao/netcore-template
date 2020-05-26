using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Utility.Models;
using Common.Utility.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Common.Utility.Extension;

namespace YourWebApiName.ApiServices.DefaultApi
{
    /// <summary>
    /// RESTapi 用户登录
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public class RestAdminController : ControllerBase
    {
        private IConfiguration Configuration { get; set; }
        /// <summary>
        /// DI
        /// </summary>
        /// <param name="configuration"></param>
        public RestAdminController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="authModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]RequestAuthModel authModel)
        {
            var apiData = new ApiResultModel();
            var key = Configuration.GetValue<string>("RestApi:Users:Key");
            var secret = Configuration.GetValue<string>("RestApi:Users:Secret");
            if (string.Equals(key, authModel.Key) && string.Equals(secret, authModel.Secret))
            {
                apiData.Result = new
                {
                    Token = Guid.NewGuid().ToString("n"),
                    ExpiresIn = DateTime.Now.GetTicks()
                };
            }
            else
            {
                apiData.Code = ErrorCodeType.KeyOrSecretError;
            }
            return Ok(apiData);
        }
    }
}