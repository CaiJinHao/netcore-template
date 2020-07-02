﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Utility.Models.OtherModels;
using System.Security.Authentication;

namespace Common.Utility.Other
{
    public class IscLmtHttpUtillib
    {
        /// <summary>
        /// 平台ip
        /// </summary>
        private string _ip;

        /// <summary>
        /// 平台端口
        /// </summary>
        private int _port = 443;

        /// <summary>
        /// 平台APPKey
        /// </summary>
        private string _appkey;

        /// <summary>
        /// 平台APPSecret
        /// </summary>
        private string _secret;

        /// <summary>
        /// 是否使用HTTPS协议
        /// </summary>
        private bool _isHttps = true;
        /// <summary>
        /// 设置信息参数
        /// </summary>
        /// <param name="appkey">合作方APPKey</param>
        /// <param name="secret">合作方APPSecret</param>
        /// <param name="ip">平台IP</param>
        /// <param name="port">平台端口，默认HTTPS的443端口</param>
        /// <param name="isHttps">是否启用HTTPS协议，默认HTTPS</param>
        /// <return></return>
        public void SetPlatformInfo(string appkey, string secret, string ip, int port = 443, bool isHttps = true)
        {
            _appkey = appkey;
            _secret = secret;
            _ip = ip;
            _port = port;
            _isHttps = isHttps;
        }

        /// <summary>
        /// HTTP GET请求
        /// </summary>
        /// <param name="uri">HTTP接口Url，不带协议和端口，如/artemis/api/resource/v1/cameras/indexCode?cameraIndexCode=a10cafaa777c49a5af92c165c95970e0</param>
        /// <param name="timeout">请求超时时间，单位：秒</param>
        /// <returns></returns>
        public byte[] HttpGet(string uri, int timeout)
        {
            Dictionary<string, string> header = new Dictionary<string, string>();

            // 初始化请求：组装请求头，设置远程证书自动验证通过
            initRequest(header, uri, "", false);

            // build web request object
            StringBuilder sb = new StringBuilder();
            sb.Append(_isHttps ? "https://" : "http://").Append(_ip).Append(":").Append(_port.ToString()).Append(uri);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(sb.ToString());
            req.KeepAlive = false;
            req.ProtocolVersion = HttpVersion.Version11;
            req.AllowAutoRedirect = false;   // 不允许自动重定向
            req.Method = "GET";
            req.Timeout = timeout * 1000;    // 传入是秒，需要转换成毫秒
            req.Accept = header["Accept"];
            req.ContentType = header["Content-Type"];

            foreach (string headerKey in header.Keys)
            {
                if (headerKey.Contains("x-ca-"))
                {
                    req.Headers.Add(headerKey + ":" + header[headerKey]);
                }
            }

            HttpWebResponse rsp = null;
            try
            {
                rsp = (HttpWebResponse)req.GetResponse();
                if (HttpStatusCode.OK == rsp.StatusCode)
                {
                    Stream rspStream = rsp.GetResponseStream();     // 响应内容字节流
                    StreamReader sr = new StreamReader(rspStream);
                    string strStream = sr.ReadToEnd();
                    long streamLength = strStream.Length;
                    byte[] response = System.Text.Encoding.UTF8.GetBytes(strStream);
                    rsp.Close();
                    return response;
                }
                else if (HttpStatusCode.Found == rsp.StatusCode || HttpStatusCode.Moved == rsp.StatusCode)  // 302/301 redirect
                {
                    string reqUrl = rsp.Headers["Location"].ToString();   // 获取重定向URL
                    WebRequest wreq = WebRequest.Create(reqUrl);          // 重定向请求对象
                    WebResponse wrsp = wreq.GetResponse();                // 重定向响应
                    long streamLength = wrsp.ContentLength;               // 重定向响应内容长度
                    Stream rspStream = wrsp.GetResponseStream();          // 响应内容字节流
                    byte[] response = new byte[streamLength];
                    rspStream.Read(response, 0, (int)streamLength);       // 读取响应内容至byte数组
                    rspStream.Close();
                    rsp.Close();
                    return response;
                }

                rsp.Close();
            }
            catch //(WebException e)
            {
                if (rsp != null)
                {
                    rsp.Close();
                }
            }

            return null;
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
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            handler.SslProtocols = SslProtocols.Tls| SslProtocols.Tls11| SslProtocols.Tls12;

            Dictionary<string, string> header = new Dictionary<string, string>();

            // 初始化请求：组装请求头，设置远程证书自动验证通过
            initRequest(header, url, postData, true);
            StringBuilder sb = new StringBuilder();
            sb.Append(_isHttps ? "https://" : "http://").Append(_ip).Append(":").Append(_port.ToString()).Append(url);

            HttpContent httpContent = new StringContent(postData);
            //httpContent.Headers.Add("content-md5", header["content-md5"]);
            //httpContent.Headers.ContentMD5 = computeContentMd5ToByte(postData);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            httpContent.Headers.ContentType.CharSet = "UTF-8";
            using (HttpClient httpClient = new HttpClient(handler))
            {
                httpClient.DefaultRequestHeaders.Add("Accept", header["Accept"]);
                foreach (string headerKey in header.Keys)
                {
                    if (headerKey.Contains("x-ca-"))
                    {
                        httpClient.DefaultRequestHeaders.Add(headerKey, header[headerKey]);
                    }
                }

                var c = System.Text.Json.JsonSerializer.Serialize(httpContent.Headers);
                Console.WriteLine(c);
                var httpResponse = httpClient.SendAsync(new HttpRequestMessage()
                {
                    Content = httpContent,
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(sb.ToString()),
                }).Result;

                if (httpResponse.IsSuccessStatusCode)
                {
                    Task<string> t = httpResponse.Content.ReadAsStringAsync();
                    return t.Result;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// HTTP Post请求
        /// </summary>
        /// <param name="uri">HTTP接口Url，不带协议和端口，如/artemis/api/resource/v1/org/advance/orgList</param>
        /// <param name="body">请求参数</param>
        /// <param name="timeout">请求超时时间，单位：秒</param>
        /// <return>请求结果</return>
        public string HttpPost(string uri, string body, int timeout)
        {
            try
            {
                Dictionary<string, string> header = new Dictionary<string, string>();

                // 初始化请求：组装请求头，设置远程证书自动验证通过
                initRequest(header, uri, body, true);

                // build web request object
                StringBuilder sb = new StringBuilder();
                sb.Append(_isHttps ? "https://" : "http://").Append(_ip).Append(":").Append(_port.ToString()).Append(uri);


                // 创建POST请求
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(sb.ToString());
                req.KeepAlive = false;
                req.ProtocolVersion = HttpVersion.Version11;
                req.AllowAutoRedirect = false;   // 不允许自动重定向
                req.Timeout = timeout * 1000;    // 传入是秒，需要转换成毫秒
                req.Method = "POST";
                req.Accept = header["Accept"];
                req.ContentType = header["Content-Type"];

                foreach (string headerKey in header.Keys)
                {
                    if (headerKey.Contains("x-ca-"))
                    {
                        req.Headers.Add(headerKey + ":" + header[headerKey]);
                    }
                }

                var t = System.Text.Json.JsonSerializer.Serialize(req.Headers);
                Console.WriteLine(t);
                if (!string.IsNullOrWhiteSpace(body))
                {
                    byte[] postBytes = Encoding.UTF8.GetBytes(body);
                    req.ContentLength = postBytes.Length;
                    Stream reqStream = null;

                    try
                    {
                        reqStream = req.GetRequestStream();
                        reqStream.Write(postBytes, 0, postBytes.Length);
                        reqStream.Close();
                    }
                    catch //(WebException e)
                    {
                        if (reqStream != null)
                        {
                            reqStream.Close();
                        }

                        return null;
                    }
                }

                HttpWebResponse rsp = null;
                try
                {
                    rsp = (HttpWebResponse)req.GetResponse();
                    if (HttpStatusCode.OK == rsp.StatusCode)
                    {
                        Stream rspStream = rsp.GetResponseStream();
                        StreamReader sr = new StreamReader(rspStream);
                        string strStream = sr.ReadToEnd();
                        long streamLength = strStream.Length;
                        byte[] response = System.Text.Encoding.UTF8.GetBytes(strStream);
                        rsp.Close();
                        var res = System.Text.Encoding.UTF8.GetString(response);
                        return res;
                    }
                    else if (HttpStatusCode.Found == rsp.StatusCode || HttpStatusCode.Moved == rsp.StatusCode)  // 302/301 redirect
                    {
                        string reqUrl = rsp.Headers["Location"].ToString();    // 如需要重定向URL，请自行修改接口返回此参数
                        WebRequest wreq = WebRequest.Create(reqUrl);
                        WebResponse wrsp = wreq.GetResponse();
                        long streamLength = wrsp.ContentLength;
                        Stream rspStream = wrsp.GetResponseStream();
                        byte[] response = new byte[streamLength];
                        rspStream.Read(response, 0, (int)streamLength);
                        rspStream.Close();
                        rsp.Close();
                        var res = System.Text.Encoding.UTF8.GetString(response);
                        return res;
                    }

                    rsp.Close();
                }
                catch //(WebException e)
                {
                    if (rsp != null)
                    {
                        rsp.Close();
                    }
                }

                return null;
            }
            finally
            {

            }
        }

        private void initRequest(Dictionary<string, string> header, string url, string body, bool isPost)
        {
            // Accept                
            string accept = "application/json";// "*/*";
            header.Add("Accept", accept);

            // ContentType  
            string contentType = "application/json; charset=UTF-8";
            header.Add("Content-Type", contentType);

            if (isPost)
            {
                // content-md5，be careful it must be lower case.
                string contentMd5 = computeContentMd5(body);
                header.Add("content-md5", contentMd5);
            }

            // x-ca-timestamp
            //string timestamp = ((DateTime.Now.Ticks - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks) / 1000).ToString();
            //var t = new System.DateTime(1970, 1, 1, 0, 0, 0, 0).Ticks;
            //var c = DateTime.Now.Ticks.ToString();
            string timestamp = ((637275756555490501 - 621355968000000000) / 1000).ToString();
            header.Add("x-ca-timestamp", timestamp);

            // x-ca-nonce
            //string nonce = System.Guid.NewGuid().ToString();
            string nonce = "dd487c4a-463c-4600-9017-112470aec20b";
            header.Add("x-ca-nonce", nonce);

            // x-ca-key
            header.Add("x-ca-key", _appkey);

            // build string to sign
            string strToSign = buildSignString(isPost ? "POST" : "GET", url, header);
            string signedStr = computeForHMACSHA256(strToSign, _secret);

            // x-ca-signature
            header.Add("x-ca-signature", signedStr);

            if (_isHttps)
            {
                // set remote certificate Validation auto pass
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(remoteCertificateValidate);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | (SecurityProtocolType)0x300 | (SecurityProtocolType)0xC00;
            }
        }

        /// <summary>
        /// 计算content-md5
        /// </summary>
        /// <param name="body"></param>
        /// <returns>base64后的content-md5</returns>
        private static string computeContentMd5(string body)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(body));
            return Convert.ToBase64String(result);
        }

        private static byte[] computeContentMd5ToByte(string body)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(body));
        }

        /// <summary>
        /// 远程证书验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cert"></param>
        /// <param name="chain"></param>
        /// <param name="error"></param>
        /// <returns>验证是否通过，始终通过</returns>
        private static bool remoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }

        /// <summary>
        /// 计算HMACSHA265
        /// </summary>
        /// <param name="str">待计算字符串</param>
        /// <param name="secret">平台APPSecet</param>
        /// <returns>HMAXH265计算结果字符串</returns>
        private static string computeForHMACSHA256(string str, string secret)
        {
            var encoder = new System.Text.UTF8Encoding();
            byte[] secretBytes = encoder.GetBytes(secret);
            byte[] strBytes = encoder.GetBytes(str);
            var opertor = new HMACSHA256(secretBytes);
            byte[] hashbytes = opertor.ComputeHash(strBytes);
            return Convert.ToBase64String(hashbytes);
        }

        /// <summary>
        /// 计算签名字符串
        /// </summary>
        /// <param name="method">HTTP请求方法，如“POST”</param>
        /// <param name="url">接口Url，如/artemis/api/resource/v1/org/advance/orgList</param>
        /// <param name="header">请求头</param>
        /// <returns>签名字符串</returns>
        private static string buildSignString(string method, string url, Dictionary<string, string> header)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(method.ToUpper()).Append("\n");
            if (null != header)
            {
                if (null != header["Accept"])
                {
                    sb.Append((string)header["Accept"]);
                    sb.Append("\n");
                }

                if (header.Keys.Contains("Content-MD5") && null != header["Content-MD5"])
                {
                    sb.Append((string)header["Content-MD5"]);
                    sb.Append("\n");
                }

                if (null != header["Content-Type"])
                {
                    sb.Append((string)header["Content-Type"]);
                    sb.Append("\n");
                }

                if (header.Keys.Contains("Date") && null != header["Date"])
                {
                    sb.Append((string)header["Date"]);
                    sb.Append("\n");
                }
            }

            // build and add header to sign
            string signHeader = buildSignHeader(header);
            sb.Append(signHeader);
            sb.Append(url);
            return sb.ToString();
        }

        /// <summary>
        /// 计算签名头
        /// </summary>
        /// <param name="header">请求头</param>
        /// <returns>签名头</returns>
        private static string buildSignHeader(Dictionary<string, string> header)
        {
            Dictionary<string, string> sortedDicHeader = new Dictionary<string, string>();
            sortedDicHeader = header;
            var dic = from objDic in sortedDicHeader orderby objDic.Key ascending select objDic;

            StringBuilder sbSignHeader = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in dic)
            {
                if (kvp.Key.Replace(" ", "").Contains("x-ca-"))
                {
                    sb.Append(kvp.Key + ":");
                    if (!string.IsNullOrWhiteSpace(kvp.Value))
                    {
                        sb.Append(kvp.Value);
                    }
                    sb.Append("\n");
                    if (sbSignHeader.Length > 0)
                    {
                        sbSignHeader.Append(",");
                    }
                    sbSignHeader.Append(kvp.Key);
                }
            }

            header.Add("x-ca-signature-headers", sbSignHeader.ToString());

            return sb.ToString();
        }

    }
}
