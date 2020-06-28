using Common.Utility.Models.OtherModels;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Common.Utility.Other
{
    public class HttpHelper
    {
        public static string GetBodyString(HttpContext httpContext)
        {
            var bodyString = string.Empty;
            using (var stream = new StreamReader(httpContext.Request.Body))
            {
                bodyString = stream.ReadToEndAsync().Result;
            }
            return bodyString;
        }

        public static async Task<string> HttpSendAsync(Uri uri, HttpMethod httpMethod, string bodyJson = "", string authorization = null)
        {
            using (var httpClient = new HttpClient())
            {
                //表头参数
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrEmpty(authorization))
                {
                    httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(authorization);
                }
                HttpContent requestHttpContent = new JsonContent(bodyJson);
                var httpResponse = await httpClient.SendAsync(new HttpRequestMessage()
                {
                    Content = requestHttpContent,
                    Method = httpMethod,
                    RequestUri = uri,
                });
                if (httpResponse.IsSuccessStatusCode)
                {
                    return await httpResponse.Content.ReadAsStringAsync();
                }
                return null;
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="body"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public static async Task<T> Get<T>(Uri uri, object body=null, string authorization = null)
        {
            var bodyJson = string.Empty;
            if (body==null)
            {
                bodyJson = System.Text.Json.JsonSerializer.Serialize(body);
            }
            var responseJson = await HttpSendAsync(uri, HttpMethod.Get, bodyJson, authorization);
            if (responseJson==null)
            {
                return default;
            }
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseJson);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="body"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public static async Task<string> Get(Uri uri, object body = null, string authorization = null)
        {
            var bodyJson = string.Empty;
            if (body == null)
            {
                bodyJson = System.Text.Json.JsonSerializer.Serialize(body);
            }
            return await HttpSendAsync(uri, HttpMethod.Get, bodyJson, authorization);
        }

        /// <summary>
        /// http请求接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public string httpClientPost(string url, string postData)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            httpContent.Headers.ContentType.CharSet = "utf-8";
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    return t.Result;
                }
            }
            return string.Empty;

        }

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {  // 总是接受  
            return true;
        }
    }
}
