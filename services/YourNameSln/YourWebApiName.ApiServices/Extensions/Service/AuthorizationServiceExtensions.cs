using Common.Utility.Models.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourWebApiName.ApiServices.Extensions.Service
{
    /// <summary>
    /// 授权认证
    /// </summary>
    public static class AuthorizationServiceExtensions
    {
        /// <summary>
        /// 添加授权策略
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthorizationService(this IServiceCollection services)
        {
            var jwtBearer = StaticConfig.AppSettings.ServiceCollectionExtension.IdentityJwt.JwtBearer;
            return services.AddAuthorization(options =>
            {
                //根据TOKEN携带的信息控制是否有权限
                {
                    //只要token携带就通过
                    options.AddPolicy("all_access", policy => policy.RequireClaim("scope"));
                    //读
                    options.AddPolicy("read_access", policy => policy.RequireClaim("scope", $"{jwtBearer.Audience}.read_access", $"{jwtBearer.Audience}.read_write_access"));
                    //写包含读
                    options.AddPolicy("read_write_access", policy => policy.RequireClaim("scope", $"{jwtBearer.Audience}.read_write_access"));
                }
                {
                    //好处就是不用在controller中，写多个 roles 
                    //第一种基于策略的授权（简单版）
                    //options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                    //options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                    //options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
                    //options.AddPolicy("A_S_O", policy => policy.RequireRole("Admin", "System", "Others"));
                    //    options.AddPolicy("BadgeEntry",
                    //          policy => policy.RequireAssertion(context =>
                    //                  context.User.HasClaim(c =>
                    //                     (c.Type == ClaimTypes.BadgeId ||
                    //                      c.Type == ClaimTypes.TemporaryBadgeId)
                    //                      && c.Issuer == "https://microsoftsecurity"));
                    //}));
                }
                {
                    //第二种复杂的策略授权
                    #region 参数
                    //读取配置文件
                    //var symmetricKeyAsBase64 = AppSecretConfig.Audience_Secret_String;
                    //var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
                    //var signingKey = new SymmetricSecurityKey(keyByteArray);
                    //var Issuer = Appsettings.app(new string[] { "Audience", "Issuer" });
                    //var Audience = Appsettings.app(new string[] { "Audience", "Audience" });

                    //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                    //// 如果要数据库动态绑定，这里先留个空，后边处理器里动态赋值
                    //var permission = new List<PermissionItem>();

                    //// 角色与接口的权限要求参数
                    //var permissionRequirement = new PermissionRequirement(
                    //    "/api/denied",// 拒绝授权的跳转地址（目前无用）
                    //    permission,
                    //    ClaimTypes.Role,//基于角色的授权
                    //    Issuer,//发行人
                    //    Audience,//听众
                    //    signingCredentials,//签名凭据
                    //    expiration: TimeSpan.FromSeconds(60 * 60)//接口的过期时间
                    //    );
                    #endregion
                    //options.AddPolicy("复杂的策略授权Permissions", policy => policy.Requirements.Add(permissionRequirement));
                }
            })
            ;
        }


        /// <summary>
        /// 添加认证服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
        {
            var jwtBearer = StaticConfig.AppSettings.ServiceCollectionExtension.IdentityJwt.JwtBearer;
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = jwtBearer.Authority;
                    options.RequireHttpsMetadata = jwtBearer.RequireHttpsMetadata;
                    options.Audience = jwtBearer.Audience;
                });
            return services;
        }
    }
}
