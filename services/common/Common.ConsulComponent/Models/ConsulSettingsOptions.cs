using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ConsulComponent.Models
{
    /// <summary>
    /// Consul配置模型类
    /// </summary>
    public class ConsulSettingsOptions
    {
        /// <summary>
        /// 服务注册地址（Consul的地址，如果是集群，取任意一个地址即可）
        /// </summary>
        public string ConsulAddress { get; set; }

        /// <summary>
        /// 服务ID  必须是唯一的
        /// </summary>
        public string ServiceId { get; set; }
        /// <summary>
        /// 该serviceName的配置应该和当前仓盒的仓房Id,和仓房名称一致
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 健康检查地址
        /// </summary>
        public string HealthCheck { get; set; }

        /// <summary>
        /// 本机ip地址+端口号，现在用的是自动获取当前服务ip,所有暂时没有用到
        /// 当前部署的地址，跟hostsettings.json地址一致
        /// </summary>
        public string LocalServerAddress { get; set; }
    }
}
