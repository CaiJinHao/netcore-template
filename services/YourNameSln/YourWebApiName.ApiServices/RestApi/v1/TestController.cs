using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YourWebApiName.ApiServices.RestApi.v1
{
    /// <summary>
    /// 临时测试控制器
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]//允许匿名访问
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 获取作者信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Name="Hawking",Age=26});
            //return Ok(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}