using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace YourWebApiName.ApiServices.HostedService
{
    /// <summary>
    /// System.Threading.Timer 定时器
    /// </summary>
    public class ThreadingTimerHostedService : IDisposable
    {
        /// <summary>
        /// 定时器
        /// </summary>
        public Timer _timer { get; set; }
        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger logger;

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _timer?.Dispose();
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
