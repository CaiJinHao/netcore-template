using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config.AppConfig
{
    /// <summary>
    /// 业务数据保留天数配置
    /// </summary>
    public class DataDayConfigModel
    {
        /// <summary>
        /// 粮情数据保留的天数
        /// </summary>
        public int GrainCableInfo { get; set; }
    }
}
