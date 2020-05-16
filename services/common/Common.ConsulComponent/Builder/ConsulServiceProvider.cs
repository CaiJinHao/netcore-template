using Consul;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Common.ConsulComponent.Builder
{
    public class ConsulServiceProvider : IServiceProvider
    {
        public ConsulClient ConsulClient { get; set; }

        public ConsulServiceProvider(Uri uri)
        {
            ConsulClient = new ConsulClient(consulConfig =>
            {
                consulConfig.Address = uri;
            });
        }

        public async Task<IList<string>> GetServicesAsync(string serviceName)
        {
            // Health 当前consul里已注册的服务，健康检查的信息也拿过来
            // HTTP API
            var queryResult = (await ConsulClient.Health.Service(serviceName, "", true)).Response;
            var result = new List<string>();
            foreach (var service in queryResult.Where(a => !string.IsNullOrEmpty(a.Service.ID)))
            {
                var _t = service.Checks.Where(a=>a.ServiceID== service.Service.ID && a.Status== HealthStatus.Passing).FirstOrDefault();
                if (_t!=null)
                {
                    result.Add(service.Service.Address + ":" + service.Service.Port);
                }
            }
            if (result.Count > 0)
            {
                return result;
            }
            else
            {
                throw new Exception($"服务不可用，当前服务状态为：{serviceName}");
            }
        }
    }
}
