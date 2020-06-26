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
    /// System.Threading.Timer 可以直接执行
    /// </summary>
    public class TimerHostedService : IHostedService
    {
        private Timer timer { get; set; }
        private ILogger logger;
        public TimerHostedService()
        { 
            logger = typeof(TimerHostedService).Logger();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //第一次执行需要等Autofac注入之后才能获取服务
            timer = new Timer(new TimerCallback(TimerTask), this, 15 * 1000, 1 * 30 * 1000);
            return Task.CompletedTask;
        }

        private  void TimerTask(object state)
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
                    logger.LogDebug("这个服务是Success");
                    //开始获取内容
                    var data = service.GetOk().Result;
                    logger.LogDebug("这个服务的结果：" + data);
                }
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex,"AutofacHelper Error：");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
