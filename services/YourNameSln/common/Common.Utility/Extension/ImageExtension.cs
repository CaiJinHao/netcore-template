using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Linq;

namespace Common.Utility.Extension
{
    /// <summary>
    /// 图片操作
    /// </summary>
    public static class ImageExtension
    {
        /// <summary>
        /// 获取图片二进制
        /// </summary>
        /// <param name="imagepath"></param>
        /// <returns></returns>
        public static byte[] ImageToBytes(this string imagepath)
        {
            //对原图片获取字节
            using (var fs = new FileStream(imagepath, FileMode.Open))
            {
                var byData = new byte[fs.Length];
                fs.Read(byData, 0, byData.Length);
                fs.Close();
                return byData;
            }
        }

        /// <summary>
        /// 转为指定质量图片的字节
        /// </summary>
        /// <param name="imagepath"></param>
        /// <param name="imagQualityValue">图像质量值</param>
        /// <returns></returns>
        public static byte[] ImageToBytes(this string imagepath,long imagQualityValue)
        {
            //图片编码信息 
            var imageCodecInfo = ImageCodecInfo.GetImageEncoders().Where(a => a.MimeType == "image/jpeg").FirstOrDefault();
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            using (MemoryStream ms = new MemoryStream())
            using (Bitmap bmp = new Bitmap(imagepath))
            {
                myEncoderParameters.Param[0] = new EncoderParameter(myEncoder, imagQualityValue);
                bmp.Save(ms, imageCodecInfo, myEncoderParameters);//转换为指定质量的图片
                //bmp.Save(msjpg3, ImageFormat.Jpeg);//将原图转换为jpeg 280kb
                return ms.ToArray();
            }
        }

        public static byte[] BitmapByte(this Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }

        /// <summary>
        /// 获取图片二进制
        /// </summary>
        /// <param name="imgPhoto">图片</param>
        /// <returns></returns>
        public static byte[] PhotoImageInsert(System.Drawing.Image imgPhoto)
        {
            //将Image转换成流数据，并保存为byte[] 
            using (MemoryStream mstream = new MemoryStream())
            {
                imgPhoto.Save(mstream, System.Drawing.Imaging.ImageFormat.Bmp);
                return mstream.ToArray();
                //byte[] byData = new Byte[mstream.Length];
                //mstream.Position = 0;
                //mstream.Read(byData, 0, byData.Length);
                //mstream.Close();
                //return byData;
            }
        }

        /// <summary>
        /// 二进制获取图片
        /// </summary>
        /// <param name="streamByte">二进制</param>
        /// <returns></returns>
        public static System.Drawing.Image ReturnPhoto(byte[] streamByte)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte))
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                return img;
            }
        }
    }
}
