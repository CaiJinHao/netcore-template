using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Common.Utility.Other
{
    public class IpHelper
    {
        /// <summary>
        /// 获取本机IPV4地址
        /// </summary>
        /// <returns>IPV4</returns>
        public static string GetLocalIP()
        {
            string HostName = Dns.GetHostName(); //得到主机名
            var IpEntry = Dns.GetHostEntry(HostName);
            foreach (var item in IpEntry.AddressList)
            {
                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {
                    return item.ToString();
                }
            }
            return string.Empty;
        }
    }
}
