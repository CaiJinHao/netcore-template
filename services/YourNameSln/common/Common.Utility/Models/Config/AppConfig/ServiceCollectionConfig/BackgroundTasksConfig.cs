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
    }
}
