using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.Config.AppConfig
{
    public class IscLmtConfig
    {
        //public string IscLmtServicesPath { get; set; }
        /// <summary>
        /// lsc流媒体的文件磁盘路径
        /// </summary>
        public string ShareFilesRootDiskPath { get; set; }
        /// <summary>
        /// 分享文件访问虚拟路径名称
        /// </summary>
        public string ShareFilesWebPath { get; set; }
        /// <summary>
        /// lsc流媒体的文件磁盘路径
        /// </summary>
        public string AutoPhotoRootDiskPath { get; set; }
        /// <summary>
        /// 自动抓拍文件访问虚拟路径名称
        /// </summary>
        public string AutoPhotoFilesWebPath { get; set; }
        /// <summary>
        /// 抓拍的文件夹名称
        /// </summary>
        public string PicturesDirName { get; set; }
        /// <summary>
        /// 录像的文件夹名称
        /// </summary>
        public string VideoDirName { get; set; }
        /// <summary>
        /// Gif的文件夹名称
        /// </summary>
        public string GifDirName { get; set; }
        /// <summary>
        /// online的ftp文件夹名称
        /// </summary>
        public string OnlineFtpDirName { get; set; }
    }
}
