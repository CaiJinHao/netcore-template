using Common.AutoUpdateUtility.Models;
using Common.AutoUpdateUtility.Services;
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

namespace AutoUpdateBox.App
{
    class Program
    {
        static void Main(string[] args)
        {
            new FileInfo(Environment.CurrentDirectory + "/Log/").Directory.CreateDirectoryInfo();
            Log4Extension.AddLog4(Path.Combine(Environment.CurrentDirectory, "log4net.config"));
            var logger = typeof(Program).Logger();

            logger.LogInformation($"Start automatic updates and check for updates every 20 seconds,Start time:{DateTime.Now}");
            var appSettings = UpdateConfig.AppSettingsJson.ReadJson<AutoUpdateAppSettings>().Result;
            var updateService = new AutoUpdateService(appSettings);
            var updateSleep = appSettings.CheckUpdateSleep * 1000;
            while (true)
            {
                Thread.Sleep(updateSleep);
                logger.LogInformation($"Start detecting updates {DateTime.Now}");
                try
                {
                    updateService.Start().Wait();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex);
                }
            }
        }
    }
}
