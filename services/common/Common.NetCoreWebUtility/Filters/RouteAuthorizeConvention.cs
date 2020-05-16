using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.NetCoreWebUtility.Filters
{
    /// <summary>
    /// 全局路由权限公约
    /// 目的是针对不同的路由，采用不同的授权策略过滤器
    /// </summary>
    public class RouteAuthorizeConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var c in application.Controllers)
            {
                if (!c.Filters.Any(e => e is AuthorizeFilter))
                {
                    //c.Filters.Add(new AuthorizeFilter("all_access"));//放到全局过滤器中添加了，这里不用添加了
                    foreach (var a in c.Actions)
                    {
                        if (!a.Filters.Any(f => f is AuthorizeFilter))
                        {
                            foreach (var attr in a.Attributes)
                            {
                                var _t = attr.GetType();
                                if (_t == typeof(HttpGetAttribute)
                                    || _t == typeof(HttpHeadAttribute)
                                    || _t == typeof(HttpOptionsAttribute))
                                {
                                    a.Filters.Add(new AuthorizeFilter("read_access"));
                                    break;
                                }
                                else if(_t == typeof(HttpPostAttribute)
                                   || _t == typeof(HttpPutAttribute)
                                   || _t == typeof(HttpPatchAttribute)
                                   || _t == typeof(HttpDeleteAttribute))
                                {
                                    a.Filters.Add(new AuthorizeFilter("read_write_access"));
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    // 写了特性，[Authorize] 或 [AllowAnonymous] ，根据情况进行权限认证
                    //走其他授权策略
                }
            }
        }
    }
}
