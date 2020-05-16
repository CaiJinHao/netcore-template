using Common.Utility.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Common.NetCoreWebUtility.Swagger
{
    public class SwaggerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties.Count == 0)
                return;

            #region 删除请求body参数

            foreach (var property in context.Type.GetProperties())
            {
                var _t = property.GetCustomAttributes(typeof(SwaggerBodyParameterPropertyAttribute), false).FirstOrDefault();
                if (_t != null)
                {
                    var swaggerParameterProperty = (SwaggerBodyParameterPropertyAttribute)_t;
                    if (!swaggerParameterProperty.Visible)
                    {
                        if (schema.Properties.ContainsKey(property.Name))
                        {
                            schema.Properties.Remove(property.Name);
                        }
                    }
                }
            }

            #endregion
        }
    }
}
