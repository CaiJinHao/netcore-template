using Common.AutoUpdateUtility.IServices;
using Common.AutoUpdateUtility.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Common.AutoUpdateUtility.Services.ProcessUtility
{
    /// <summary>
    /// linux 使用 需要安装supervisor并进行配置
    /// </summary>
    public class ForSupervisorProcessService : IProcessService
    {
        public void RegisterService(RegisterServiceModel paramModel)
        {
            //linux 不需要注册服务，需要手动配置supervisor
        }

        public void StartProcess(string processId)
        {
            //linux 重启进程只需要杀掉进程就可以自动重启，这是supervisor的自动重启机制
            StopProcess(processId);
        }

        public void StopProcess(string processId)
        {
            //linux 使用的是supervisor 不用停止就可以更新文件
            Process.GetProcessById(Convert.ToInt32(processId)).Kill();
        }
    }
}
