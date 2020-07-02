using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Utility.Other
{
    public class FileWaterMark
    {
        /// <summary>
        /// 添加备注--加在图片下方
        /// </summary>
        /// <param name="model">添加的文字信息类</param>
        /// <param name="picturePath">需要添加备注的图片路径</param>
        /// <returns>合成图片的路径</returns>
        public static string SetWaterMark(RemarkModel model, string picturePath)
        {
            string fontContent = "";
            string fileName = "";
            try
            {
                //制作备注图片
                Image picutre = Image.FromFile(picturePath);
                int pwidth = picutre.Width;
                Image imgRemark;
                if (pwidth < 700)
                    imgRemark = new Bitmap(picutre.Width, 120);
                else
                    imgRemark = new Bitmap(picutre.Width, 60);
                Graphics g = Graphics.FromImage(imgRemark);
                Font font = new Font("微软雅黑", 10, (System.Drawing.FontStyle.Bold));
                Brush bush = new SolidBrush(Color.White);
                g.Clear(Color.Black);
                fontContent = "监控编码：" + model.ChannelCode;
                g.DrawString(fontContent, font, bush, 5, 5);
                fontContent = "监控名称：" + model.ChannelName;
                if (pwidth < 700)
                    g.DrawString(fontContent, font, bush, 5, 30);
                else
                    g.DrawString(fontContent, font, bush, 220, 5);
                fontContent = "IP地址：" + model.ChannelIp;
                if (pwidth < 700)
                    g.DrawString(fontContent, font, bush, 5, 55);
                else
                    g.DrawString(fontContent, font, bush, 480, 5);
                if (model.CaptureType == 0)
                    fontContent = "抓取模式：自动抓取（" + model.CaptureInfo + "）";
                else
                    fontContent = "抓取模式：手动抓取（" + model.CaptureInfo + "）";
                if (pwidth < 700)
                    g.DrawString(fontContent, font, bush, 5, 80);
                else
                    g.DrawString(fontContent, font, bush, 5, 30);
                //fontContent = "抓取时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                fontContent = "抓取时间：" + model.CaptureTime ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (pwidth < 700)
                    g.DrawString(fontContent, font, bush, 5, 105);
                else
                    g.DrawString(fontContent, font, bush, 220, 30);
                fontContent = model.Memo;//异常报备备注
                if (pwidth < 700)
                    g.DrawString(fontContent, font, bush, 5, 140);
                else
                    g.DrawString(fontContent, font, bush, 480, 30);
                g.Save();
                g.Dispose();
                string remarkPath = picturePath.Substring(0, picturePath.LastIndexOf(".")) + "remark.jpg";
                imgRemark.Save(remarkPath, ImageFormat.Jpeg);

                //与原图片进行合成
                Image imgCbn;
                if (pwidth < 700)
                    imgCbn = new Bitmap(picutre.Width, picutre.Height + 130);
                else
                    imgCbn = new Bitmap(picutre.Width, picutre.Height + 60);
                Graphics gh = Graphics.FromImage(imgCbn);
                gh.DrawImage(picutre, 0, 0, picutre.Width, picutre.Height);
                gh.DrawImage(imgRemark, 0, picutre.Height);
                gh.Save();
                gh.Dispose();
                fileName = picturePath.Substring(0, picturePath.LastIndexOf(".")) + "_cbn.jpg";
                imgCbn.Save(fileName, ImageFormat.Jpeg);
                picutre.Dispose();
                imgRemark.Dispose();
                imgCbn.Dispose();
                File.Delete(picturePath);
                File.Delete(remarkPath);
            }
            catch
            {

            }
            return fileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error">错误信息</param>
        /// <returns>路径</returns>
        public static string SetErrorPicture(string error, string path, string reportMessage)
        {
            Image errorPic = new Bitmap(700, 570);
            Graphics g = Graphics.FromImage(errorPic);
            Font font = new Font("微软雅黑", 20, (System.Drawing.FontStyle.Bold));
            Brush bush = new SolidBrush(Color.White);
            g.Clear(Color.Black);
            string str = "";
            if (reportMessage != "")
            {
                string[] reportMessages = reportMessage.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                str += reportMessages[0] + "，";
                str += "因" + reportMessages[1] + "已报备，";
                str += "并进行" + reportMessages[2] + "处理。";
                str += "开始时间：" + reportMessages[3] + "，";
                str += "预计恢复时间：" + reportMessages[4] + "。";
            }
            else
            {
                if (error == "最近操作没有异常发生")
                    str = "失败原因：摄像头不在线";
                else
                    str = "失败原因：" + error;
            }
            g.DrawString(str, font, bush, new Rectangle(75, 220, 560, 130));
            g.Save();
            g.Dispose();
            FileInfo fi = new FileInfo(path);
            if (!Directory.Exists(fi.DirectoryName))
            {
                Directory.CreateDirectory(fi.DirectoryName);
            }
            errorPic.Save(path, ImageFormat.Jpeg);
            return path;
        }
    }
}
