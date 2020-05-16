using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.ConsulComponent.LoadBalancer
{
    public interface ILoadBalancer
    {
        string Resolve(IList<string> services);
    }
}
