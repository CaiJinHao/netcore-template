using Common.AutoUpdateUtility.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.AutoUpdateUtility.IServices
{
    public interface IProcessService
    {
        /// <summary>
        /// 安装完成重启进程
        /// </summary>
        void StartProcess(string processId);
        /// <summary>
        /// 安装之前停止进程
        /// </summary>
        void StopProcess(string processId);
        /// <summary>
        /// windows使用服务注册参数
        /// </summary>
        /// <param name="paramModel"></param>
        void RegisterService(RegisterServiceModel paramModel);
    }
}
