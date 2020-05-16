using System;
using System.Collections.Generic;
using System.Text;

namespace Common.AutoUpdateUtility.Models
{
    public class AutoUpdateAppSettings
    {
        /// <summary>
        /// 新版本路径（有可能多个系统放到同一个更新里面）
        /// https://jpclyj.com/iotapp
        /// </summary>
        public string NewVersionUrl { get; set; }
        /// <summary>
        /// 云盒程序进程信息和版本信息接口
        /// http://localhost:9016/api/v1/Process
        /// http://localhost:6001/api/Process
        /// </summary>
        public string ProcessApi { get; set; }
        /// <summary>
        /// 检查更新时间间隔 秒
        /// </summary>
        public int CheckUpdateSleep { get; set; }
        /// <summary>
        /// 当前更新程序运行的环境
        /// </summary>
        public EnumRunEnvironment RunEnvironment { get; set; }
        /// <summary>
        /// 程序服务名称windows用，windows时才赋值
        /// </summary>
        public RegisterServiceModel RegisterServiceParam { get; set; }
    }

    public enum EnumRunEnvironment
    { 
        Linux=0,
        Windows=1
    }
}
