using Common.NetCoreWebUtility.IServices;
using Common.Utility.Models.Config;
using Common.Utility.Models.Config.AppConfig;
using Common.Utility.RequestModels;
using Common.Utility.ResponesModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Utility.Extension;
using IdentityModel.Client;
using System.IO;
using Common.Utility.Models.User;
using System.Linq;

namespace Common.NetCoreWebUtility.Services
{
    public class ToKenService : IToKenService
    {
        //private ILogger logger { get; set; }
        private PasswordTokenConfig passwordToken { get; set; }

        public ToKenService()
        {
            passwordToken = StaticConfig.AppSettings.ServiceCollectionExtension.IdentityJwt.PasswordToken;
            //logger = typeof(ToKenService).Logger();
        }

        public async Task<ResponesToKenModel> GetTokenAsync(RequestAuthModel requestAuthModel)
        {
            var rdata = new ResponesToKenModel();

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(passwordToken.Address);
            if (disco.IsError)
            {
                rdata.Error = disco.Error;
                //logger.LogError(disco.Error);
                return rdata;
            }
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = passwordToken.ClientId,
                ClientSecret = passwordToken.ClientSecret,
                Scope = passwordToken.Scope,
                UserName = requestAuthModel.Key,
                Password = requestAuthModel.Secret
            });

            if (tokenResponse.IsError)
            {
                rdata.Error = tokenResponse.ErrorDescription;
                return rdata;
            }
            rdata.AccessToken = tokenResponse.AccessToken;
            rdata.ExpiresIn = tokenResponse.ExpiresIn;
            rdata.TokenType = tokenResponse.TokenType;
            var p_path = Path.Combine(StaticConfig.ContentRootPath, AppFileConfig.PrivateKey);
            rdata.Pkey = await p_path.ReadFileAsync();
            return rdata;
        }

        public string GetValueByToken(TokenInfoType  tokenInfoType)
        {
            string infoType = string.Empty;
            switch (tokenInfoType)
            {
                case TokenInfoType.UserId:
                    infoType = ClaimConfig.UserId;
                    break;
                case TokenInfoType.RoleId:
                    infoType = ClaimConfig.RoleId;
                    break;
                case TokenInfoType.RoleName:
                    infoType = ClaimConfig.RoleName;
                    break;
                case TokenInfoType.UserInfo:
                    infoType = ClaimConfig.UserInfo;
                    break;
                default:
                    break;
            }
            var claim = UserHttpContext.Current.User.Claims.Where(c => c.Type == infoType).FirstOrDefault();
            if (claim == null)
            {
                throw new Exception("用户授权异常，获取不多Token内容信息");
            }
            return claim.Value;
        }
    }
}
