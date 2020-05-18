using Common.Utility.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Common.NetCoreWebUtility.Swagger
{
    /// <summary>
    /// 查询参数是否显示
    /// </summary>
    public class SwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            #region 删除查询参数

            if (operation.Parameters.Count > 0)
            {
                foreach (var model in context.ApiDescription.ActionDescriptor.Parameters)
                {
                    foreach (var property in model.ParameterType.GetProperties())
                    {
                        var _t = property.GetCustomAttributes(typeof(SwaggerQueryParameterPropertyAttribute), false).FirstOrDefault();
                        if (_t != null)
                        {
                            var swaggerParameterProperty = (SwaggerQueryParameterPropertyAttribute)_t;
                            var _p = operation.Parameters.Where(a => a.Name == property.Name).FirstOrDefault();
                            if (_p != null)
                            {
                                if (!swaggerParameterProperty.Visible)
                                {
                                    operation.Parameters.Remove(_p);
                                }
                            }
                            else
                            {
                                //针对对象的处理，对象字段名称是查不到的
                                if (!swaggerParameterProperty.Visible)
                                {
                                    var obj_fiels = operation.Parameters.Where(a => a.Name.StartsWith(property.Name)).ToList();
                                    foreach (var _field in obj_fiels)
                                    {
                                        operation.Parameters.Remove(_field);
                                    }
                                }
                            }
                        }
                    }
                }
            }


            #endregion

            #region Swagger授权过期器处理

            //if (operation.Security == null)
            //var oAuthRequirements = new List<OpenApiSecurityRequirement>() { 
            //     new OpenApiSecurityRequirement()
            //};
            //                            //{

            //                            //      {"oauth2", new List<string> { "openid", "profile", "examinationservicesapi" }}
            //                            //};
            //operation.Security.Add(oAuthRequirements);

            #endregion

            #region Swagger 文件上传处理

            //var files = context.ApiDescription.ActionDescriptor.Parameters.Where(n => n.ParameterType == typeof(IFormFile)).ToList();
            //if (files.Count > 0)
            //{
            //    for (int i = 0; i < files.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            operation.Parameters.Clear();
            //        }
            //        operation.Parameters.Add(new OpenApiParameter
            //        {
            //            Name = files[i].Name,
            //            In = "formData",
            //            Description = "Upload File",
            //            Required = true,
            //            Type = 
            //        });

            //    }
            //    operation.Consumes.Add("multipart/form-data");
            //}

            #endregion
        }
    }
}
