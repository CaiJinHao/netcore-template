using Common.AutoUpdateUtility.IServices;
using Common.AutoUpdateUtility.Models;
using Common.Utility.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Common.AutoUpdateUtility.Services.ProcessUtility
{
    /// <summary>
    /// 需要借助nssm.exe程序
    /// </summary>
    public class ForNssmProcessService : IProcessService
    {
        public void RegisterService(RegisterServiceModel paramModel)
        {
            //nssm的相对启动程序文件
            var nssmRelativeStartFile = new DirectoryInfo(Environment.CurrentDirectory).Parent + paramModel.StartFile;
            typeof(ForNssmProcessService).Logger().LogInformation("启动项目路径："+nssmRelativeStartFile);
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo("nssm.exe", $"install {paramModel.ServiceName} {nssmRelativeStartFile}"),
            };
            process.Start();
            process.Close();
            //安装完之后等等在启动
            Thread.Sleep(5000);
            StartProcess(paramModel.ServiceName);
        }

        public void StartProcess(string serviceName)
        {
            var process = new Process() {
                 StartInfo=new ProcessStartInfo("nssm.exe",$"start {serviceName}"),
            };
            process.Start();
            process.Close();
        }

        public void StopProcess(string serviceName)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo("nssm.exe", $"stop {serviceName}"),
            };
            process.Start();
            process.Close();
            //由于stop 之后需要时间去关闭端口所以需要等待
            Thread.Sleep(5000);
        }
    }
}
