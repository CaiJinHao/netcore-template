using Common.Utility.Extension;
using Common.Utility.Models.Config;
using Common.Utility.Models.Log;
using Common.Utility.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Common.NetCoreWebUtility.Middleware
{
    /// <summary>
    /// 记录HTTP请求
    /// </summary>
    public class HttpRequstRecordMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private readonly IHttpInfo httpInfo;

        public HttpRequstRecordMiddleware(RequestDelegate _next, IHttpInfo _httpInfo)
        {
            next = _next;
            httpInfo = _httpInfo;
            logger =typeof(HttpRequstRecordMiddleware).Logger();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var httpRequest = context.Request;
            //过滤请求
            if (
                   StaticConfig.AppSettings.HttpRequstRecordMiddleware.EnabledRequstRecord
                    && httpRequest.Path.Value.Contains("api"))
            {
                httpRequest.EnableBuffering();
                var originalBody = context.Response.Body; //原始Stream

                try
                {
                    var rlInfo = new HttpRequestLogInfoModel(LogInfoType.API)
                    {
                        Ip = httpInfo.GetIp(),
                        Url = httpRequest.Path.ToString().TrimEnd('/').ToLower(),
                        Method = httpRequest.Method,
                        ContentType = httpRequest.ContentType,
                        Headers = GetHeaders(httpRequest)
                    };

                    // 存储请求数据
                    await RequestDataLog(context, rlInfo);

                    using (var ms = new MemoryStream())
                    {
                        context.Response.Body = ms;

                        await next(context);

                        // 存储响应数据
                        await ResponseDataLog(ms, rlInfo);

                        ms.Position = 0;//重置指针
                        await ms.CopyToAsync(originalBody);//将新的Stream给到原始Stream变量
                    }
                }
                catch (Exception ex)
                {
                    typeof(HttpRequstRecordMiddleware).Logger().LogError(ex);
                }
                finally
                {
                    context.Response.Body = originalBody; //将新的Stream给到原始响应
                }
            }
            else
            {
                await next(context);
            }
        }

        private async Task RequestDataLog(HttpContext context, HttpRequestLogInfoModel rlInfo)
        {
            var request = context.Request;
            var _queryData = request.QueryString.Value;
            var _bodyData = await new StreamReader(request.Body).ReadToEndAsync();
            if (!string.IsNullOrEmpty(_queryData) || !string.IsNullOrEmpty(_bodyData))
            {
                rlInfo.QueryData = System.Net.WebUtility.UrlDecode(_queryData);
                rlInfo.BodyData = _bodyData;

                request.Body.Position = 0;//重置指针
            }
        }

        private List<string> GetHeaders(HttpRequest httpRequest)
        {
            var headers = new List<string>();
            foreach (var item in httpRequest.Headers)
            {
                headers.Add(item.Serialize());
            }
            return headers;
        }

        private async Task ResponseDataLog(MemoryStream ms, HttpRequestLogInfoModel rlInfo)
        {
            ms.Position = 0;//读之前重置指针
            var _responseBody = await new StreamReader(ms).ReadToEndAsync();
            if (!string.IsNullOrEmpty(_responseBody))
            {
                Parallel.For(0, 1, e =>
                {
                    rlInfo.ResponseBody = _responseBody;
                    logger.LogInfo(rlInfo);
                });
            }
        }
    }
}
