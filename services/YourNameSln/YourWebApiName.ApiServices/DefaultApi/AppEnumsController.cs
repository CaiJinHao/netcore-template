using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Utility.Models;
using Common.Utility.Other;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YourWebApiName.ApiServices.DefaultApi
{
    /// <summary>
    /// 应用程序中的枚举类型
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/appenums")]
    [ApiController]
    public class AppEnumsController : ControllerBase
    {
        /// <summary>
        /// 给下拉列表使用
        /// </summary>
        /// <param name="Oprator"></param>
        /// <returns></returns>
        [HttpGet("{Oprator}")]
        public IActionResult Get(int Oprator)
        {
            switch (Oprator)
            {
                case 1000:
                    {
                        var data = EnumsHelper.GetEnumListByDescription<EnumIsNot>();
                        return Ok(new ApiResultModel()
                        {
                            Result = data
                        });
                    }
                default:
                    return Ok(new ApiResultModel()
                    {
                        Result = new List<dynamic>()
                        {
                            new { Text = "文本", Value = 1 }
                        }
                    });
            }
        }
    }
}