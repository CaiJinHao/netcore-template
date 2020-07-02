using Common.Utility.Models.Config;
using Common.Utility.Models.Events;
using log4net.Config;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Common.Utility.Extension
{
    /// <summary>
    /// Logger Extention
    /// </summary>
    public static class Log4Extension
    {
        private static ILoggerRepository loggerRepository { get; set; }
        public static void AddLog4(this ILoggingBuilder loggingBuilder,string log4netConfigPath)
        {
            //过滤掉系统默认的一些日志
            //Filter out some of the system's default logs
            //loggingBuilder.AddFilter("System", LogLevel.Warning);
            //loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
            loggingBuilder.AddLog4Net(log4NetConfigFile: log4netConfigPath);
            loggerRepository = log4net.LogManager.CreateRepository(
              Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
        }

        public static void AddLog4(string log4netConfigPath)
        {
            loggerRepository = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            XmlConfigurator.Configure(loggerRepository, new FileInfo(log4netConfigPath));
        }

        public static ILogger Logger(this Type _t)
        {
            //TODO:是否需要优化到上面
            //var _loggerRepository = log4net.LogManager.CreateRepository(
            //    Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            return new Log4NetLogger(new Log4NetProviderOptions()
            {
                LoggerRepository = loggerRepository.Name,
                Name = _t.FullName
            });
        }

        /// <summary>
        /// 自定义扩展错误日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="exception"></param>
        public static void LogError(this ILogger logger,Exception exception)
        {
            logger.LogError(exception.Message + exception.StackTrace);
        }

        public static void LogError(this ILogger logger, string info, Exception exception)
        {
            logger.LogError(info + exception.Message + exception.StackTrace);
        }

        /// <summary>
        /// 自定义扩展错误日志
        /// 注意发送邮件的时候 使用logError调用的抛出异常就不能调用邮件，否则将logError的方法去除
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="info"></param>
        public static void LogErrorInfo(this ILogger logger, string info)
        {
            logger.LogError(info);
        }

        /// <summary>
        /// 自定义扩展信息日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="info"></param>
        public static void LogInfo(this ILogger logger, object info)
        {
            var enableInfo = StaticConfig.AppSettings.LoggerConfig.EnableInfo;
            if (enableInfo)
            {
                if (info.GetType() == typeof(string))
                {
                    logger.LogInformation(info.ToString());
                }
                else
                {
                    logger.LogInformation(info.Serialize().Escape());
                }
            }
        }

        /// <summary>
        /// 自定义警告信息日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="warningInfo"></param>
        public static void LogWarn(this ILogger logger, object warningInfo)
        {
            var enableInfo = StaticConfig.AppSettings.LoggerConfig.EnableInfo;
            if (enableInfo)
            {
                if (warningInfo.GetType() == typeof(string))
                {
                    logger.LogWarning(warningInfo.ToString());
                }
                else
                {
                    logger.LogWarning(warningInfo.Serialize().Escape());
                }
            }
        }

        /// <summary>
        /// 自定义Debug信息
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="warningInfo"></param>
        public static void LogDebuging(this ILogger logger, object debugInfo)
        {
            //由于日志 递增速度，只有调试的时候才使用，或者配置到配置文件中
            var enableDebug= StaticConfig.AppSettings.LoggerConfig.EnableDebug;
            if (enableDebug)
            {
                if (debugInfo.GetType() == typeof(string))
                {
                    logger.LogDebug(debugInfo.ToString());
                }
                else
                {
                    logger.LogDebug(debugInfo.Serialize().Escape());
                }
            }
        }

        /// <summary>
        /// 发送错误警告
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="info"></param>
        public static void SendErrorWarning(this ILogger logger, string info)
        {
            logger.LogError("发送错误警告："+info);
            StaticEvents.SendWarning();
        }
    }
}
