using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using YourWebApiName.IdentityServer.Models;

namespace YourWebApiName.IdentityServer.Service
{
    /// <summary>
    /// 自定义用户名密码验证器
    /// </summary>
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ISystemClock _clock;

        public CustomResourceOwnerPasswordValidator(ISystemClock clock)
        {
            _clock = clock;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var _httpClient = new HttpClient();
            var requestUrl = $"{StaticConfigModel.AppSettings.VerifyUserUrl}/{context.UserName}/{context.Password}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "验证用户服务响应失败,请联系管理员解决");
                return;
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<dynamic>(responseContent);
            if ((int)apiResult.Code != 0)
            {
                //验证失败
                context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient, apiResult.Msg);
            }
            else
            {
                //验证通过返回结果 
                var user_id = (string)apiResult.Result.user_id;//用户唯一标识
                var role_id = (string)apiResult.Result.role_id;//用户唯一标识
                var role_name = (string)apiResult.Result.role_name;//角色名称
                var user_info = (string)apiResult.Result.user_info;//用户信息jsonString

                /*
                 Claim type必须小写，否则添加不到token中
                 必须配置ApiResource 的ClaimTypes 否则不会携带
                 */
                context.Result = new GrantValidationResult(
                    user_id,
                    OidcConstants.AuthenticationMethods.Password,
                    _clock.UtcNow.UtcDateTime, new List<Claim>()
                    {
                       new Claim(ClaimConfigModel.UserId,user_id),
                       new Claim(ClaimConfigModel.RoleId,role_id),
                       new Claim(ClaimConfigModel.RoleName, role_name),
                       new Claim(ClaimConfigModel.UserInfo, user_info)
                    });
            }
        }
    }
}
