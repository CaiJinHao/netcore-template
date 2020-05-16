using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.AutoUpdateUtility.Services
{
    /// <summary>
    /// 下载文件
    /// </summary>
    public class DownloadFileService
    {
        public static async Task<bool> DownloadFile(Uri uri,string localfile)
        {
            using (var httpClient=new HttpClient())
            {
                var streamResponse= await httpClient.GetStreamAsync(uri);
                using (var localStream=File.Create(localfile))
                {
                    streamResponse.CopyTo(localStream);
                }
            }
            return true;
        }
    }
}
