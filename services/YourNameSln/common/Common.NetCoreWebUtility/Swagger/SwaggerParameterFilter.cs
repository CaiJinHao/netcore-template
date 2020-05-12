using Common.Utility.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.OpenApi.Interfaces;

namespace Common.NetCoreWebUtility.Swagger
{
    /// <summary>
    /// Swagger参数处理
    /// </summary>
    public class SwaggerParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            //if (context.ParameterInfo == null) return;
        }
    }
}
