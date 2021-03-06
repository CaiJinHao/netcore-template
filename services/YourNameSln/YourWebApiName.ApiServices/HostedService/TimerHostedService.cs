﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace YourWebApiName.ApiServices.HostedService
{
    /// <summary>
    /// System.Timers.Timer 定时器
    /// </summary>
    public class TimerHostedService : IDisposable
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
        /// <returns></returns>
        public Task StopAsync()
        {
            _timer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
