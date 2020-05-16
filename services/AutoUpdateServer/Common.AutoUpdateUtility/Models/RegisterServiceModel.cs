using System;
using System.Collections.Generic;
using System.Text;

namespace Common.AutoUpdateUtility.Models
{
    /// <summary>
    /// 注册服务参数
    /// </summary>
    public class RegisterServiceModel
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 服务启动文件
        /// 启动位置是相对nssm.exe的位置，需要获取当前路径的上一级路径 拼接为绝对路径
        /// </summary>
        public string StartFile{ get; set; }
    }
}
