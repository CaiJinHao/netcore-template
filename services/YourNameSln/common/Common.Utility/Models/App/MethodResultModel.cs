using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models
{
    public class MethodResultModel
    {
        public MethodResultModel()
        {
            Success = true;
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
