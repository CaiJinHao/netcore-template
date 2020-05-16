using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using Common.Utility.Extension;
using Common.Utility.Models;

namespace Common.NetCoreWebUtility.Middleware
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if (e == null) return;
            typeof(ExceptionHandlerMiddleware).Logger().LogError(e);
            await WriteExceptionAsync(context, e).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception e)
        {
            if (e is UnauthorizedAccessException)
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else if (e is Exception)
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            context.Response.ContentType = "application/json";

            var rData = new ApiResultModel(ErrorCodeType.ServerError).Serialize();
            context.Response.StatusCode = (int)HttpStatusCodeType.ServerError;
            await context.Response.WriteAsync(rData).ConfigureAwait(false);
        }
    }
}
