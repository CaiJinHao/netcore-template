using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config.AppConfig
{
    /// <summary>
    /// 后台任务配置
    /// </summary>
    public  class BackgroundTasksConfig
    {
        /// <summary>
        /// 是否启动
        /// </summary>
        public bool EnabledDeviceDetectionService { get; set; }
        /// <summary>
        /// 后台服务执行的延迟时间 秒为单位
        /// </summary>
        public int HostedServiceDueTimeSecond { get; set; }
        /// <summary>
        /// 该服务的时间间隔 分钟为单位
        /// </summary>
        public int StatisticsGrainStatusHostedServicePeriodMinute { get; set; }
        /// <summary>
        /// 该服务的时间间隔 分钟为单位
        /// </summary>
        public int ViewRecordsHostedServicePeriodMinute { get; set; }
    }
}
