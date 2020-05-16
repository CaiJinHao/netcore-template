using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Common.Utility.Extension
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 将对象中的值赋值给自己
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">自己</param>
        /// <param name="tobj">对象</param>
        public static void CloneTo<T>(this object sourceObj, T toObj)
        {
            if (sourceObj == null)
            {
                return;
            }
            var sourceInfo = sourceObj.GetType().GetProperties();
            var toInfo = toObj.GetType().GetProperties();
            foreach (var toItem in toInfo)
            {
                var toFiled = sourceInfo.Where(a => a.Name == toItem.Name).FirstOrDefault();
                var v = toItem.GetValue(sourceObj, null);
                if (v != null)
                {
                    toFiled.SetValue(toObj, v, null);
                }
            }
        }
    }
}
