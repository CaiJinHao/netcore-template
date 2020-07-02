using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.Models.Config.AppConfig
{
    /// <summary>
    /// 系统文件路径
    /// </summary>
    public class AppFilePathsModel
    {
        private string _exportExcelPath;
        /// <summary>
        /// 导出Excel文件路径 按日期存储方便清理缓存
        /// </summary>
        public string ExportExcelPath
        {
            get { return _exportExcelPath+ DateTime.Now.ToString("yyyy_MM_dd"); }
            set { _exportExcelPath = value; }
        }
    }
}
