using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models
{
    /*
      10000-19999 为APP服务级错误
      20000-29999 为Controller服务模块代码错误
      30000-39999 为类库方法错误
             */

    /// <summary>
    /// API 错误码
    /// </summary>
    public enum ErrorCodeType
    {
        /// <summary>
        /// 请求响应成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 服务器错误 用在无法确定错误的地方
        /// </summary>
        ServerError = 10000,
        /// <summary>
        /// 操作选项错误，没有该选型
        /// </summary>
        OptionError = 20000,
        /// <summary>
        /// 参数错误 验证参数不通过
        /// </summary>
        ParamsError = 20001,
        /// <summary>
        /// Key或者Secret错误
        /// </summary>
        KeyOrSecretError = 20002,
        /// <summary>
        /// 验证签名失败
        /// </summary>
        VerifySignatureError=20003,
        /// <summary>
        /// 创建失败，请检查参数正确性
        /// </summary>
        PostError = 20004,
        PutError = 20005,
        DeleteError = 20006,
        /// <summary>
        /// 请求(调用)外部返回数据错误
        /// </summary>
        RequestResultError = 30001,
    }

    /// <summary>
    /// HTTP 返回状态码对应
    /// </summary>
    public enum HttpStatusCodeType
    {
        /// <summary>
        /// BadRequest
        /// </summary>
        ParamsError = 400,

        ServerError =500,
    }
}
