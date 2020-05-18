using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Common.NetCoreWebUtility.Swagger
{
    /// <summary>
    /// 查询去除必填项
    /// </summary>
    public class QueryParamsSwaggerOprationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters.Count>0)
            {
                foreach (var model in context.ApiDescription.ActionDescriptor.Parameters)
                {
                    var propertyList = model.ParameterType.GetProperties()
                         .Where(property => property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault() != null);
                    foreach (var property in propertyList)
                    {
                        var _p = operation.Parameters.Where(a => a.Name == property.Name).FirstOrDefault();
                        if (_p != null)
                        {
                            _p.Required = false;
                        }
                    }
                }
            }
        }
    }
}
