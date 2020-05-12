using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.Events
{
    /*
        全局委托、回调
        解决循环依赖问题
         */

    /// <summary>
    /// 全局委托、回调
    /// 解决循环依赖问题
    /// </summary>
    public class StaticEvents
    {
        /// <summary>
        /// 预警处理
        /// </summary>
        public static Action SendWarning { get; set; }
    }

    /// <summary>
    /// 警告类型
    /// </summary>
    [Flags]
    public enum EnumWarningType
    {
        None = 0,
        邮件预警 = 1,
        短信预警 = 2
    }
}
