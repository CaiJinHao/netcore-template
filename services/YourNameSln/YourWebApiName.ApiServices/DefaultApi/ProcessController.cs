using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YourWebApiName.ApiServices.DefaultApi
{
    /// <summary>
    /// 进程信息,程序版本更新
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]//允许匿名访问
    public class ProcessController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var process = Process.GetCurrentProcess();
            return Ok(new
            {
                ProcessId = process.Id,
                /*
                3个地方需要统一：这里、更新压缩包的名称就是版本号、更新服务器的version.json中的版本号
                这3个地方必须一致
                这个版本必须和更新服务器的版本保持一致，否则会导致一直更新，系统就不能用了
                */
                /*
                 新增数据删除策略
                 */
                CurrentVersion = 1.10
            });
        }
    }
}