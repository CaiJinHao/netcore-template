using Common.Utility.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.AppConfig
{
    /// <summary>
    /// 预警配置
    /// </summary>
    public class WarningConfigModel
    {
        /// <summary>
        /// 预警方式 可多选
        /// </summary>
        public EnumWarningType WarningType = EnumWarningType.短信预警 | EnumWarningType.邮件预警;
    }
}
