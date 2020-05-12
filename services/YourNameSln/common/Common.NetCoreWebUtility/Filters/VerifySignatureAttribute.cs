using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Common.Utility.Extension;
using Common.Utility.Models.Config;
using Common.Utility.Encryption;
using Microsoft.Extensions.Logging;
using Common.Utility.Models;

namespace Common.NetCoreWebUtility.Filters
{
    /// <summary>
    /// 验证签名
    /// </summary>
    public class VerifySignatureAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = context.HttpContext;
            switch (httpContext.Request.Method)
            {
                case "GET":
                    {//该请求暂时不验证签名
                        await base.OnActionExecutionAsync(context, next);
                        return;
                    }
            }
            var dataResult = new ApiResultModel(ErrorCodeType.ServerError);
            try
            {
                var requestData = GetDataParamStr(httpContext);

                #region 签名示例
                //私匙签名  公匙验签
                //var privateKeyFile = PlatformConfig.ContentRootPath + GlobalConfig.platformConfig.appData.sshPathPrivate;
                //var localSign = SignatureKit.RSASignCharSet(requestData.Data, privateKeyFile, null, "RSA2");
                //requestData.Sign = localSign;
                //var urlEncodeLocalSign = WebUtility.UrlEncode(localSign);//数据签名
                #endregion

                var _check = ValidateSign(requestData.time);
                //开始利用公匙验证签名有效性
                var publicKeyFile = StaticConfig.ContentRootPath + StaticConfig.AppSettings.VerifySignature.PublicKeyFile;
                //_check = _check ? SignatureKit.RSACheckContent(requestData.Data, requestData.Sign, publicKeyFile, "UTF-8", "RSA2") : false;
                var rsa = new RSAEncrypt(RSAType.RSA2, Encoding.UTF8, null, publicKeyFile);
                _check = _check ? rsa.Verify(requestData.Data, requestData.Sign) : false;
                if (_check)
                {
                    await base.OnActionExecutionAsync(context, next);
                    return;
                }
            }
            catch (Exception ex)
            {
                typeof(VerifySignatureAttribute).Logger().LogError(ex);
                dataResult.Result = ex;
                await Response(httpContext, dataResult);
                return;
            }
            dataResult.Code = ErrorCodeType.VerifySignatureError;
            await Response(httpContext, dataResult);
        }

        /// <summary>
        /// 获取客户端提交过来的参数
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private SignRequestData GetDataParamStr(HttpContext httpContext)
        {
            switch (httpContext.Request.Method)
            {
                case "GET":
                    {
                        return GetRequestParamsQueryString(httpContext);
                    }
                case "POST":
                default:
                    {
                        return GetRequestParamsBody(httpContext);
                    }
            }
        }

        /// <summary>
        /// 获取GET请求 发来的参数
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private SignRequestData GetRequestParamsQueryString(HttpContext httpContext)
        {
            var result = new SignRequestData();
            var paramList = new Dictionary<string, dynamic>();
            foreach (var key in httpContext.Request.Query.Keys)
            {
                paramList.Add(key, httpContext.Request.Query[key].ToString());
            }
            result.Sign = paramList.GetValueOrDefault("sign").ToString();
            result.time = paramList.GetValueOrDefault("time").ToString();
            result.Data = GetQueryParamsStr(new SortedDictionary<string, dynamic>(paramList));
            return result;
        }

        /// <summary>
        /// 获取排序后的参数准备验签
        /// </summary>
        /// <param name="sortedParams"></param>
        /// <returns></returns>
        private string GetQueryParamsStr(IDictionary<string, dynamic> sortedParams)
        {
            var queryParams = new List<string>();
            foreach (var item in sortedParams)
            {
                //为空的不进行签名
                //if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(Convert.ToString(item.Value)) && item.Key != "sign")
                //    queryParams.Add($"{item.Key}={item.Value}");

                //为了导致对象签名的问题 都转为字符串进行签名
                queryParams.Add($"{item.Key}={item.Value.Serialize()}");
            }
            return string.Join("&", queryParams);
        }

        /// <summary>
        /// 获取POST 请求发来的参数
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private SignRequestData GetRequestParamsBody(HttpContext httpContext)
        {
            var result = new SignRequestData();

            httpContext.Request.EnableBuffering();//重置读取位置
            httpContext.Request.Body.Position = 0;
            var bodyString = string.Empty;
            using (var stream = new StreamReader(httpContext.Request.Body))
            {
                bodyString = stream.ReadToEnd();
            }
            var data = bodyString.Deserialize<dynamic>();//转为动态类型
            var queryParams = Utility.Other.ReflectHelper.ConvertToDictionary(data);//转换为QueryString验签
            result.time = data.Time;
            result.Sign = httpContext.Request.Headers["sign"];//httpContext.Request.Query["sign"].ToString();//从url中获取签名
            //result.Data = GetQueryParamsStr(new SortedDictionary<string, dynamic>(queryParams));
            result.Data = GetQueryParamsStr(queryParams);
            return result;
        }

        /// <summary>
        /// 验证时间有效性
        /// </summary>
        /// <param name="timeStr">时间戳</param>
        /// <returns></returns>
        private bool ValidateSign(string timeStr)
        {
            long.TryParse(timeStr, out long requestTime);
            var nowTime = DateTime.Now.AddSeconds(-StaticConfig.AppSettings.VerifySignature.ExpirationTime).GetTotolMillis();
            if (requestTime > nowTime)
            {
                return true;
            }
            return false;
        }

        private async Task Response(HttpContext httpContext, object ObjResult)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCodeType.ParamsError;
            //httpContext.Response.ContentType = "text/plain; charset=utf-8";
            httpContext.Response.ContentType = "application/json; charset=utf-8";
            await httpContext.Response.WriteAsync(ObjResult.Serialize(), Encoding.UTF8);
        }

        public static string GetBodyString(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();//重置读取位置
            httpContext.Request.Body.Position = 0;
            var bodyString = string.Empty;
            using (var stream = new StreamReader(httpContext.Request.Body))
            {
                bodyString = stream.ReadToEnd();
            }
            return bodyString;
        }
    }

    public class SignRequestData
    {
        /// <summary>
        /// 客户端发来的签名
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 可以是json  也可以是querystring  总之是要生成签名和验签的数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 客户端的时间戳 用来验证有效时间
        /// </summary>
        public string time { get; set; }
    }
}
