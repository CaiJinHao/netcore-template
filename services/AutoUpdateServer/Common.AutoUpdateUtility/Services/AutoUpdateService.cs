using Common.AutoUpdateUtility.IServices;
using Common.AutoUpdateUtility.Models;
using Common.AutoUpdateUtility.Services;
using Common.AutoUpdateUtility.Services.ProcessUtility;
using Common.Utility.Extension;
using Common.Utility.Other;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Common.AutoUpdateUtility.Services
{
    public class AutoUpdateService
    {
        private ILogger logger { get; set; }

        /// <summary>
        /// 更新包的域名
        /// </summary>
        private AutoUpdateAppSettings appSettings { get; set; }
        /// <summary>
        /// 正在更新
        /// </summary>
        private bool Updating { get; set; }
        public AutoUpdateService(AutoUpdateAppSettings _appSettings)
        {
            appSettings = _appSettings;
            logger = typeof(AutoUpdateService).Logger();
            //判断需要的文件夹是否存在
            new FileInfo(UpdateConfig.UpdateFiles).Directory.CreateDirectoryInfo();
            InstallService();
        }

        /// <summary>
        /// 开始更新
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            try
            {
                if (Updating)
                {
                    //正在更新时不再检测
                    logger.LogInformation($"Update in progress, intercepts redundant updates {DateTime.Now}");
                    return;
                }
                var newVersionInfo = await GetNewVersionAsync();
                var versionInfo = await GetVersionInfoAsync();
                versionInfo.NewVersion = newVersionInfo.NewVersion;
                versionInfo.NewVersionDescription = newVersionInfo.NewVersionDescription;
                //验证是否有新版本需要更新,如果版本没有增加，会导致一直更新，系统就不能用了
                if (versionInfo.CurrentVersion < versionInfo.NewVersion)
                {
                    Updating = true;
                    logger.LogInformation($"Start updating the current version {versionInfo.CurrentVersion},The latest version {versionInfo.NewVersion},Update description: {versionInfo.NewVersionDescription},Update time:{DateTime.Now}");
                    //logger.LogInformation($"开始更新，当前版本:{versionInfo.CurrentVersion},最新版本:{versionInfo.NewVersion},更新时间:{DateTime.Now},更新描述：{versionInfo.NewVersionDescription}");
                    await Update(versionInfo);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
            finally
            {
                Updating = false;
            }
        }

        /// <summary>
        /// 开始更新
        /// </summary>
        /// <param name="version">要更新的版本号</param>
        /// <returns></returns>
        private async Task Update(VersionInfo versionInfo)
        {
            try
            {
                var downloadUrl = new Uri($"{appSettings.NewVersionUrl}/{versionInfo.NewVersion}.zip");
                //本地更新包文件
                var localfile = Path.Combine(UpdateConfig.UpdateFiles, versionInfo.NewVersion + ".zip");
                var b = await DownloadFileService.DownloadFile(downloadUrl, localfile);
                if (b)
                {
                    var processService = GetProcessService();
                    if (appSettings.RunEnvironment== EnumRunEnvironment.Windows)
                    {
                        versionInfo.ProcessId = appSettings.RegisterServiceParam.ServiceName;
                    }
                    //杀掉进程进行更新
                    processService.StopProcess(versionInfo.ProcessId);
                    //将更新包解压到web服务器目录
                    ZipService.DeCompress(localfile, versionInfo.UpdateDirectory);
                    //更新完之后杀掉进程,进程会自动重启
                    processService.StartProcess(versionInfo.ProcessId);
                    //更新当前程序成功后，版本会自动变为最新版本，每次更新的时候需要+0.01
                    //logger.LogInformation($"版本{versionInfo.NewVersion}更新成功");
                    logger.LogInformation($"Version {versionInfo.NewVersion} was updated successfully");
                }
                else
                {
                    logger.LogError("下载更新文件失败");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
        }

        #region 版本

        /// <summary>
        /// 获取最新更新包的版本信息
        /// </summary>
        /// <returns></returns>
        private async Task<VersionInfo> GetNewVersionAsync()
        {
            var uri = new Uri($"{appSettings.NewVersionUrl}/version.json");
            var respones = await HttpHelper.HttpSendAsync(uri, HttpMethod.Get);
            return JsonSerializer.Deserialize<VersionInfo>(respones);
        }

        /// <summary>
        /// 获取当前系统版本信息和进程信息
        /// </summary>
        /// <returns></returns>
        private async Task<VersionInfo> GetVersionInfoAsync()
        {
            var uri = new Uri(appSettings.ProcessApi);
            var respones = await HttpHelper.HttpSendAsync(uri, HttpMethod.Get);
            return JsonSerializer.Deserialize<VersionInfo>(respones);
        }
        #endregion

        /// <summary>
        /// 获取操作进程的服务
        /// </summary>
        private IProcessService GetProcessService()
        {
            switch (appSettings.RunEnvironment)
            {
                case EnumRunEnvironment.Windows:
                    return new ForNssmProcessService();
                default:
                    return new ForSupervisorProcessService();
            }
        }

        /// <summary>
        /// 将要更新的程序安装到windows服务
        /// </summary>
        private void InstallService()
        {
            //存在也不会抛异常
            var service = GetProcessService();
            service.RegisterService(appSettings.RegisterServiceParam);
        }
    }
}
