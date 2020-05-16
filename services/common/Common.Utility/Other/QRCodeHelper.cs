using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utility.Other
{
    public class QRCodeHelper
    {
        /// <summary>
        /// 生成二维码 返回字节
        /// </summary>
        public static Bitmap GenerateQRCodeBitmap(string qrCodeContent)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeContent, QRCodeGenerator.ECCLevel.Q, true))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        Bitmap qrCodeImage = qrCode.GetGraphic(20);
                        return qrCodeImage;
                    }
                }
            }
        }
    }
}
