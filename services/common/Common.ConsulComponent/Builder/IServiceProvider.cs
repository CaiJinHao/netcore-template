using Consul;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConsulComponent.Builder
{
    public interface IServiceProvider
    {
        ConsulClient ConsulClient { get; set; }
        Task<IList<string>> GetServicesAsync(string serviceName);
    }
}
