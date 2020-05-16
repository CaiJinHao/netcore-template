using Common.Utility.Models.OtherModels;
using Common.Utility.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
    }
}
