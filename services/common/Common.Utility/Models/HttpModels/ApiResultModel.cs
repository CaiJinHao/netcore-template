using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models
{
    /// <summary>
    /// API 结果
    /// </summary>
    public class ApiResultModel
    {
        private ErrorCodeType _code;
        public ErrorCodeType Code { get { return _code; } set { _code = value; SetErrorCodeTypeMsg(); } }
        public string Msg { get; set; }
        public dynamic Result { get; set; }

        public ApiResultModel()
        {
            Code =  ErrorCodeType.Success;
            SetErrorCodeTypeMsg();
        }

        public ApiResultModel(ErrorCodeType _code)
        {
            Code = _code;
            SetErrorCodeTypeMsg();
        }

        public ApiResultModel(ErrorCodeType _code,string msg)
        {
            Code = _code;
            if (msg != null && msg.Length > 0)
            {
                Msg = msg;
            }
            else
            {
                SetErrorCodeTypeMsg();
            }
        }

        public ApiResultModel(ErrorCodeType _code, dynamic _data)
        {
            Code = _code;
            Result = _data;
            SetErrorCodeTypeMsg();
        }

        public void SetErrorCodeTypeMsg()
        {
            var _codeMsg = string.Empty;
            switch (Code)
            {
                case ErrorCodeType.Success:
                    _codeMsg = "请求响应成功";
                    break;
                case ErrorCodeType.ServerError:
                    _codeMsg = "服务器错误,无法确定错误的类型";
                    break;
                case ErrorCodeType.OptionError:
                    _codeMsg = "选项错误，没有该选项";
                    break;
                case ErrorCodeType.ParamsError:
                    _codeMsg = "参数错误,验证参数不通过";
                    break;
                case ErrorCodeType.KeyOrSecretError:
                    _codeMsg = "Key或者Secret错误";
                    break;
                case ErrorCodeType.VerifySignatureError:
                    _codeMsg = "签名验证失败";
                    break;
                case ErrorCodeType.PostError:
                    _codeMsg = "创建失败，请检查参数正确性";
                    break;
                case ErrorCodeType.PutError:
                    _codeMsg = "更新失败，请检查参数正确性";
                    break;
                case ErrorCodeType.RequestResultError:
                    _codeMsg = "请求(调用)外部返回数据错误";
                    break;
                default:
                    break;
            }
            Msg = _codeMsg;
        }
    }
}
