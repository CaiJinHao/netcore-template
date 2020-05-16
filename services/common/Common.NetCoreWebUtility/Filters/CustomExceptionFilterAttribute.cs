using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Utility.Extension;
using Microsoft.Extensions.Logging;
using Common.Utility.Models;

namespace Common.NetCoreWebUtility.Filters
{
    /// <summary>
    /// 特性异常过滤器 用在方法和类型上
    /// 暂时没有用到
    /// </summary>
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            //异常有没有被处理过
            if (!context.ExceptionHandled)
            {
                typeof(CustomExceptionFilterAttribute).Logger().LogError(context.Exception);
                {
                    //if (this.IsAjaxRequest(context.HttpContext.Request))//检查请求头
                    //{
                    //    context.Result = new BadRequestObjectResult(new ApiResult(ErrorCodeType.ServerError));
                    //}
                    //else
                    //{
                    //    //响应视图
                    //}
                }
                context.Result = new ObjectResult(new ApiResultModel(ErrorCodeType.ServerError,context.Exception?.Message))
                {
                    StatusCode = (int)HttpStatusCodeType.ServerError
                };
                context.ExceptionHandled = true;//标记为已处理
            }
            return Task.CompletedTask;
        }

        private bool IsAjaxRequest(HttpRequest request)
        {
            //Accept 是不是用这个可以判断  当时text时
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}
