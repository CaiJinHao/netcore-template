using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Extension
{
    /// <summary>
    /// 业务异常
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(BusinessErrorCodeType businessErrorCode,string message)
            :base(message)
        {
            ErrorCode = businessErrorCode;
        }

        public BusinessException(BusinessErrorCodeType businessErrorCode, string message, Exception ex)
            : base(message, ex)
        {
            ErrorCode = businessErrorCode;
        }

        public BusinessErrorCodeType ErrorCode { get; set; }
    }
}
