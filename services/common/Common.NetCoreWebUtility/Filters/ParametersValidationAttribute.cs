using Common.Utility.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.NetCoreWebUtility.Filters
{
    /// <summary>
    /// 参数验证(Model验证) 为所有Action添加
    /// </summary>
    public class ParametersValidationAttribute : ActionFilterAttribute
    {
        private bool validate = true;

        public bool Validate { get => validate; set => validate = value; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var validateMethods = new List<string> { "POST" };
            var method = context.HttpContext.Request.Method.ToUpper();
            if (validateMethods.Contains(method) && Validate)
            {
                if (!context.ModelState.IsValid)
                {
                    var dataResult = new ApiResultModel()
                    {
                        Code = ErrorCodeType.ParamsError,
                    };
                    foreach (var field in context.ModelState)
                    {
                        foreach (var error in field.Value.Errors)
                        {
                            dataResult.Msg += $"{field.Key}:{string.Join(",", error.ErrorMessage)};";
                        }
                    }
                    context.Result = new ObjectResult(dataResult) { StatusCode = (int)HttpStatusCodeType.ParamsError };
                }
            }
        }
    }
}
