using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Config.AppConfig
{
    public class MiddlewareExtensionFile
    {
        private int _staticFileCachePeriod = 10;
        /// <summary>
        /// 静态文件缓存时间 单位:秒
        /// </summary>
        public int StaticFileCachePeriod { get => _staticFileCachePeriod; set => _staticFileCachePeriod = value; }
    }
}
