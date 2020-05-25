using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourWebApiName.IServices.IDbServices;

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
        public ISysRolesService sysRolesService { get; set; }
        /// <summary>
        /// 获取作者信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await sysRolesService.GetModelsAsync(new Models.RequestModels.SysRolesRequestModel() { });
            return Ok(new { Name = "Hawking", Age = 26, Data = data });
            //return Ok(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}