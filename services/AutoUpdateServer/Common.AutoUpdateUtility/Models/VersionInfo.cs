using System;
using System.Collections.Generic;
using System.Text;

namespace Common.AutoUpdateUtility.Models
{
    public class VersionInfo
    {
        /// <summary>
        /// 上传文件的地方，最新的版本(上传之后更改)
        /// </summary>
        public double NewVersion { get; set; }
        /// <summary>
        /// 新版本描述
        /// </summary>
        public string NewVersionDescription { get; set; }
        /// <summary>
        /// 更新系统得当前的版本
        /// </summary>
        public double CurrentVersion { get; set; }
        /// <summary>
        /// 更新系统得进程id/服务名称
        /// </summary>
        public string ProcessId { get; set; }
        /// <summary>
        /// 要更新的程序目录,有程序接口获取
        /// </summary>
        public string UpdateDirectory { get; set; }
    }
}
