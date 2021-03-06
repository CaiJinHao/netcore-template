﻿using Common.Utility.Extension;
using Common.Utility.Models;
using Common.Utility.Models.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Common.NetCoreWebUtility.Filters
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public class MvcExceptionsFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                typeof(MvcExceptionsFilter).Logger().LogError("CustomExceptionFilterAttribute:" + context.Exception);
                context.Result = new ObjectResult(new ApiResultModel(ErrorCodeType.ServerError, context.Exception?.Message))
                {
                    StatusCode = (int)HttpStatusCodeType.ServerError
                };
                context.ExceptionHandled = true;//标记为已处理
            }
        }
    }
}
