using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utility.Other
{
    public class RemarkModel
    {
        public string ChannelNo { set; get; }//通道号
        public string ChannelCode { set; get; }//通道编号
        public string ChannelName { set; get; }//通道名称
        public string ChannelIp { set; get; }//通道IP
        public int CaptureType { set; get; }//抓取类型(0：自动抓取 1：手动抓取)
        public string CaptureInfo { set; get; }//抓取信息(自动抓取类型，例如：测温 手动抓取人，例如：张三)
        public string CaptureTime { set; get; }// 抓取时间
        public string Memo { set; get; }// 备注
    }
}
