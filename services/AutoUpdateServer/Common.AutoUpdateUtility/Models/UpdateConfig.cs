using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.AutoUpdateUtility.Models
{
    public class UpdateConfig
    {
        /// <summary>
        /// 更新包存放路径
        /// </summary>
        public static string UpdateFiles
        {
            get
            {
                //不许需要加两个“/”  /文件夹名称/
                return AppDomain.CurrentDomain.BaseDirectory + "/update/";
            }
        }

        /// <summary>
        /// 系统配置文件
        /// </summary>
        public static string AppSettingsJson = "appsettings.json";
    }
}
