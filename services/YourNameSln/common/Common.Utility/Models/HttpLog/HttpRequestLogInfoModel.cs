using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Log
{
    /// <summary>
    /// INFO 日志
    /// </summary>
    public class HttpRequestLogInfoModel
    {
        public string Url { get; set; }
        public string QueryData { get; set; }
        public string BodyData { get; set; }
        public string Ip { get; set; }
        public LogInfoType LogType { get; set; }
        public string LogTypeMsg { get; set; }
        public string Method { get; set; }
        public string ContentType { get; set; }
        public List<string> Headers { get; set; }
        public string ResponseBody { get; set; }
        public HttpRequestLogInfoModel(LogInfoType requestLogType)
        {
            LogType = requestLogType;
            SetRequestLogTypeMsg();
        }

        private void SetRequestLogTypeMsg()
        {
            switch (LogType)
            {
                case LogInfoType.API:
                    LogTypeMsg = "API";
                    break;
                case LogInfoType.Other:
                    LogTypeMsg = "其他";
                    break;
                default:
                    break;
            }
            LogTypeMsg += "请求";
        }
    }

    public enum LogInfoType
    { 
        API=1,
        Other=2
    }
}
