using Common.Utility.Extension;
using Common.Utility.Models.Config;
using Common.Utility.Models.Config.AppConfig;
using Common.Utility.RequestModels;
using Common.Utility.ResponesModels;
using IdentityModel.Client;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common.NetCoreWebUtility.Services
{
    public class ToKenService
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

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            var client = new HttpClient(handler);
            var disco = await client.GetDiscoveryDocumentAsync(passwordToken.Address);
            if (disco.IsError)
            {
                rdata.IsError = disco.IsError;
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
                rdata.IsError = tokenResponse.IsError;
                return rdata;
            }
            rdata.AccessToken = tokenResponse.AccessToken;
            rdata.ExpiresIn = tokenResponse.ExpiresIn;
            rdata.TokenType = tokenResponse.TokenType;
            var p_path = Path.Combine(StaticConfig.ContentRootPath, AppFileConfig.PrivateKey);
            rdata.Pkey = await p_path.ReadFileAsync();
            return rdata;
        }
    }
}
