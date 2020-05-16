using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Extension
{
    /// <summary>
    /// 时间扩展
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 星期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string Week(this DateTime dateTime)
        {
            string week = string.Empty;
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = "周一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "周二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "周三";
                    break;
                case DayOfWeek.Thursday:
                    week = "周四";
                    break;
                case DayOfWeek.Friday:
                    week = "周五";
                    break;
                case DayOfWeek.Saturday:
                    week = "周六";
                    break;
                case DayOfWeek.Sunday:
                    week = "周日";
                    break;
                default:
                    week = "N/A";
                    break;
            }
            return week;
        }

        /// <summary>
        /// 获取指定时间的毫秒数
        /// </summary>
        /// <param name="_t"></param>
        /// <returns></returns>
        public static long GetTotolMillis(this DateTime _t)
        {
            DateTime dtFrom = new DateTime(1970, 1, 1);
            var jsTime = _t.ToUniversalTime();//转换成和js 时间格式相同的
            long currentMillis = (jsTime.Ticks - dtFrom.Ticks) / 10000;
            return currentMillis;
        }

        /// <summary>
        /// 计算时间差
        /// </summary>
        /// <param name="DateTime1">结束时间</param>
        /// <param name="DateTime2">开始时间</param>
        /// <returns></returns>
        public static int DateDiffDay(this DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts.Days;
        }

        /// <summary>
        /// 获取分钟差
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static int DateDiffMinutes(this DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts.Minutes;
        }

        /// <summary>
        /// 获取时间差
        /// </summary>
        /// <param name="DateTime1">无所谓谁在前谁在后</param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static TimeSpan DateDiff(this DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            //var dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return ts;
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="_time"></param>
        /// <returns></returns>
        public static long GetTicks(this DateTime _time)
        {
            var startTime = new DateTime(1970, 1, 1);
            return (_time.Ticks - startTime.Ticks) / 10000000 - 8 * 60 * 60;
        }

        /// <summary>
        /// 根据时间戳获取当前时间
        /// </summary>
        /// <param name="_ticks"></param>
        /// <returns></returns>
        public static DateTime GetTimeByTicks(this long _ticks)
        {
            var startTime = new DateTime(1970, 1, 1);
            return startTime.AddTicks((_ticks + 8 * 60 * 60) * 10000000);
        }

        /// <summary>
        /// 获取UTC时间
        /// mongodb时间是标准utc +0:00  中国时区:+8:00
        /// </summary>
        /// <param name="_time"></param>
        /// <returns></returns>
        public static DateTime Utc(this DateTime _time)
        {
           return DateTime.SpecifyKind(_time, DateTimeKind.Utc);
        }
    }
}
