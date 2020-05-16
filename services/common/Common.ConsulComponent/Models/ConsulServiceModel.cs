using Common.Utility.Models;
using Consul;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ConsulComponent.Models
{
    public class ConsulServiceModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string Node { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// 用于健康注销和启动
        /// </summary>
        public string CheckID { get; set; }
        public EnumIsNot IsStart { get; set; }
    }
}
