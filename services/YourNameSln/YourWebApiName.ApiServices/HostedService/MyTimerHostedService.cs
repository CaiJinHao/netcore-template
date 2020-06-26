using Common.Utility.Autofac;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YourWebApiName.IServices.IDbServices;
using Common.Utility.Extension;
using Microsoft.Extensions.Logging;

namespace YourWebApiName.ApiServices.HostedService
{
    /// <summary>
    /// System.Timers.Timer 不能直接执行
    /// </summary>
    public class MyTimerHostedService : IHostedService
    {
        private System.Timers.Timer timer { get; set; }
        private ILogger logger;
        public MyTimerHostedService()
        {
            logger= typeof(MyTimerHostedService).Logger();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new System.Timers.Timer(1 * 30 * 1000);
            //timer.Elapsed += async (sender, e) =>
            //{
            //    await Test();
            //};
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
            return Task.CompletedTask;
        }

        private async Task Test()
        {
            try
            {
                var service = AutofacHelper.GetService<ISysRolesService>();
                if (service == null)
                {
                    logger.LogDebug("这个服务是Error");
                }
                else
                {
                    logger.LogDebug("这个Test服务是Success");
                    //开始获取内容
                    var data = await service.GetOk();
                    logger.LogDebug("这个服务的结果：" + data);
                }
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex, "AutofacHelper Error：");
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var service = AutofacHelper.GetService<ISysRolesService>();
                if (service == null)
                {
                    logger.LogDebug("这个服务是Error");
                }
                else
                {
                    logger.LogDebug("这个Timer_Elapsed服务是Success");
                    //开始获取内容
                    var data = service.GetOk().Result;
                    logger.LogDebug("这个服务的结果：" + data);
                }
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex, "AutofacHelper Error：");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Stop();
            timer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
