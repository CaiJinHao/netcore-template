using YourWebApiName.IdentityServer.Models;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace YourWebApiName.IdentityServer.Service
{
    /// <summary>
    /// 身份验证配置
    /// </summary>
    public class IdentityConfigService
    {
        public static Models.AppSettingsModel AppSettings { get; set; }

        /// <summary>
        /// 自定义身份资源 OpenID Connect 使用
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResourceResources()
        {
            var customProfile = new IdentityResource(
                name: "custom.profile",
                displayName: "Custom profile",
                claimTypes: new[] { "name", "authlist", "status" });

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
                customProfile
            };
        }

        /// <summary>
        /// scopes define the API resources in your system
        /// 作用域定义系统中的API资源，能够获取什么属性和Clients有直接关系
        /// （JwtClaimTypes）请求此资源时应包含的相关用户身份单元信息列表。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources(params ApiResourceConfigModel[] apiResourceConfigs)
        {
            foreach (var item in apiResourceConfigs)
            {
                //必须用构造函数 其他方式无效
                //如果想token中携带Cliams 必须在这里定义(item.ClaimTypes)，否则不会携带
                yield return new ApiResource(item.Name, item.DisplayName,new string[] {
                    JwtClaimTypes.Role,
                    JwtClaimTypes.AuthenticationTime,
                    JwtClaimTypes.AuthenticationMethod,
                    JwtClaimTypes.AuthorizedParty,
                    JwtClaimTypes.SessionId,
                    ClaimConfigModel.UserId, 
                    ClaimConfigModel.RoleId,
                    ClaimConfigModel.RoleName
                }) {
                    //定义API资源的scope,可结合授权策略是否通过GET、POST、ALL请求
                    // API 验证Token Scope使用
                    Scopes = {
                        new Scope(){ Name=$"{item.Name}.read_access",DisplayName=$"Read only access to {item.Name}"},//只读
                        new Scope(){ Name=$"{item.Name}.read_write_access",DisplayName=$"Write only access to {item.Name}"},//只写
                    }
                };
            }
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(params ClientConfigModel[] clientModels)
        {
            foreach (var item in clientModels)
            {
                var client = new Client
                {
                    AllowAccessTokensViaBrowser = true,
                    //该用户拥有不同的scope，可使用任意scope进行请求，使用哪个scope取决于客户端请求参数
                    //scope跟API资源名称进行对应
                    //客户端请求TOKEN Scope
                    //可以结合授权策略（Policy）使用
                    AllowedScopes = new[] {
                        $"{item.ApiName}.read_access",
                        $"{item.ApiName}.read_write_access",
                    },
                    ClientId = item.ClientId,
                    ClientName = item.ClientName,
                    AccessTokenLifetime = item.AccessTokenLifetime,//Token的有效时间秒/单位
                    ClientSecrets =
                    {
                        new Secret(item.ClientSecrets.Sha256())
                    },
                };
                switch (item.AllowedGrantTypes)
                {
                    case "client_credentials":
                        {
                            client.AllowedGrantTypes = GrantTypes.ClientCredentials;
                        }
                        break;
                    default:
                        client.AllowedGrantTypes = GrantTypes.ResourceOwnerPassword;
                        break;
                }
                yield return client;
            }
        }
    }
}
