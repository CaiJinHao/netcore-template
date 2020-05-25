using YourWebApiName.IdentityServer.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace YourWebApiName.IdentityServer.Service
{
    /// <summary>
    /// TODO:不知道干嘛的
    /// </summary>
    public class CustomProfileService : IProfileService
    {
        /// <summary>
        /// 验证用户信息Claims
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="action">回调</param>
        /// <returns></returns>
        private async Task ValidateClaims(IEnumerable<Claim> claims, Action<dynamic> action)
        {
            var key = claims.FirstOrDefault(x => x.Type == "UserName").Value;
            var secret = claims.FirstOrDefault(x => x.Type == "Password").Value;

            var _httpClient = new HttpClient();
            var response = await _httpClient.GetAsync($"{StaticConfigModel.AppSettings.VerifyUserUrl}/{key}/{secret}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Resource server is not working!");
            }
            var content = await response.Content.ReadAsStringAsync();
            var userApiResult = System.Text.Json.JsonSerializer.Deserialize<dynamic>(content);
            if (userApiResult.Code == 0)
            {
                //调用此方法以后内部会进行过滤，只将用户请求的Claim加入到 context.IssuedClaims 集合中 这样我们的请求方便能正常获取到所需Claim
                action(userApiResult.data);
            }
        }


        /// <summary>
        /// 验证用户是否有效 例如：token创建或者验证
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            await ValidateClaims(context.Subject.Claims, (user) => {
                context.IsActive = true;
            });
        }

        /// <summary>
        /// 只要有关用户的身份信息单元被请求（例如在令牌创建期间或通过用户信息终点），就会调用此方法
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public  Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //判断是否有请求Claim信息
            if (context.RequestedClaimTypes.Any())
            {
                context.AddRequestedClaims(context.Subject.Claims);

                //根据用户唯一标识查找用户信息
                //调用此方法以后内部会进行过滤，只将用户请求的Claim加入到 context.IssuedClaims 集合中 
                //这样我们的请求方便能正常获取到所需Claim
                //await ValidateClaims(context.Subject.Claims, (user) =>
                //{
                //    context.AddRequestedClaims(context.Subject.Claims);
                //});
            }
            return Task.CompletedTask;
        }

    }
}
