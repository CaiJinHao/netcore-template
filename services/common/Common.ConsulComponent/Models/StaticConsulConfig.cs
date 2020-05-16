using Common.ConsulComponent.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ConsulComponent.Models
{
    public class StaticConsulConfig
    {
        public static ConsulSettingsOptions ConsulSettings { get; set; }
        public static ConsulServiceProvider ServiceProvider
        {
            get
            {
                return new ConsulServiceProvider(new Uri(ConsulSettings.ConsulAddress));
            }
        }
    }
}
