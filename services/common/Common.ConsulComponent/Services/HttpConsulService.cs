using Common.ConsulComponent.Builder;
using Common.ConsulComponent.LoadBalancer;
using Common.ConsulComponent.Models;
using Common.Utility.Extension;
using Common.Utility.Models;
using Common.Utility.Models.User;
using Common.Utility.Other;
using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Primitives;

namespace Common.ConsulComponent.Services
{
    public class HttpConsulService
    {
        /// <summary>
        /// 构建服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="routeUrl"></param>
        /// <returns></returns>
        private static IServiceBuilder BuildService(string serviceName)
        {
            //调用仓级服务
            var myService = StaticConsulConfig.ServiceProvider.CreateServiceBuilder(builder =>
            {
                builder.ServiceName = serviceName;
                // 指定负载均衡器
                builder.LoadBalancer = TypeLoadBalancer.RandomLoad;
                // 指定Uri方案
                builder.UriScheme = Uri.UriSchemeHttp;
            });
            return myService;
        }

        /// <summary>
        /// 将返回结果转为对象
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="apiResultJson"></param>
        /// <returns></returns>
        private static TResult ConvertResult<TResult>(string apiResultJson)
        {
            var _t = typeof(TResult);
            if (_t == typeof(ApiResultModel))
            {//不需要转换Result的数据
                var data = apiResultJson.Deserialize<TResult>();
                return data;
            }
            //需要转换
            return ((JsonElement)apiResultJson.Deserialize<ApiResultModel>().Result).ToString().Deserialize<TResult>();
        }

        /// <summary>
        /// 根据服务和api地址获取数据
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="routeUrl"></param>
        /// <returns></returns>
        public static async Task<TResult> ResultAsync<TResult>(string serviceName, string routeUrl)
        {
            var myService = BuildService(serviceName);
            var method = UserHttpContext.Current.Request.Method.ToUpper();
            var bodyJson = string.Empty;
            Uri uri = null;
            switch (method)
            {
                case "GET":
                    {
                        uri = await myService.BuildUriAsync($"/api/v1/{HttpUtility.UrlDecode(routeUrl)}" + UserHttpContext.Current.Request.QueryString);
                    }
                    break;
                case "DELETE":
                case "POST":
                case "PUT":
                    {
                        uri = await myService.BuildUriAsync($"/api/v1/{HttpUtility.UrlDecode(routeUrl)}");
                        bodyJson = HttpHelper.GetBodyString(UserHttpContext.Current);
                    }
                    break;
                default:
                    return default;
            }
            var httpMethod = new HttpMethod(method);
            var authorization = new StringValues();
            UserHttpContext.Current.Request.Headers.TryGetValue("Authorization", out authorization);
            var apiResultJson = await HttpHelper.HttpSendAsync(uri, httpMethod, bodyJson, authorization);
            return ConvertResult<TResult>(apiResultJson);
        }

        /// <summary>
        /// 获取consul中注册的所有服务
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<ConsulServiceModel>> GetServicesAsync()
        {
            var consulClient = StaticConsulConfig.ServiceProvider.ConsulClient;
            var queryResult = await consulClient.Agent.Services();
            var healthStatuResult = (await consulClient.Health.State(Consul.HealthStatus.Any)).Response;
            var data = queryResult.Response.Where(a => !string.IsNullOrEmpty(a.Key))
                .Select(a => new ConsulServiceModel()
                {
                    ID = a.Value.ID,
                    Port = a.Value.Port,
                    Address = a.Value.Address,
                    Name = a.Value.Service,
                }).ToList();
            foreach (var item in data)
            {
                item.IsStart = EnumIsNot.No;
                var t = healthStatuResult.Where(a => a.ServiceID == item.ID).FirstOrDefault();
                if (t != null)
                {
                    item.Node = t.Node;
                    item.Status = t.Status.Status;
                    item.CheckID = t.CheckID;
                    if (t.Status == HealthStatus.Passing)
                    {
                        item.Status = "已启动";
                        item.IsStart =  EnumIsNot.Yes;
                    }
                    else if (t.Status == HealthStatus.Warning)
                    {
                        item.Status = "服务预警";
                    }
                    else if (t.Status == HealthStatus.Critical)
                    {
                        item.Status = "启动中";
                    }
                    else if (t.Status == HealthStatus.Maintenance)
                    {
                        item.Status = "维护中";
                    }
                }
                else
                {
                    item.Status = "已停止";
                }
            }
            return data;
        }

        /// <summary>
        /// 健康注销
        /// </summary>
        /// <param name="CheckId"></param>
        /// <returns></returns>
        public static async Task<bool> CheckDeregister(string serviceID)
        {
            var consulClient = StaticConsulConfig.ServiceProvider.ConsulClient;
            var result = await consulClient.Agent.CheckDeregister("service:" + serviceID);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 健康注册
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static async Task<bool> CheckRegister(ConsulServiceModel service)
        {
            var consulClient = StaticConsulConfig.ServiceProvider.ConsulClient;
            var result = await consulClient.Agent.CheckRegister(new AgentCheckRegistration()
            {
                ID = "service:" + service.ID,
                Name = service.Name,
                ServiceID = service.ID,
                TCP = service.Address + ":" + service.Port,
                Interval = TimeSpan.FromSeconds(10)
            });
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
    }
}
