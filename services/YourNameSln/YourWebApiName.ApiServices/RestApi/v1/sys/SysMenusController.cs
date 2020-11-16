

using System;
using Common.Utility.Models.HttpModels;
using Common.Utility.Extension;
using Common.Utility.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YourWebApiName.IServices.IDbServices;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;


namespace YourWebApiName.ApiServices.RestApi.v1
{
    /// <summary>
    /// 系统_菜单
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/sysmenus")]
    [ApiController]
    public class SysMenusController : ControllerBase
    {
        private string route = "api/v1/sysmenus";
        /// <summary>
        /// 服务
        /// </summary>
        public ISysMenusService sysMenusService { get; set; }

        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <param name="oprator">
        /// 为0时默认查分页数据
        /// 为1时默认查不分页数据
        /// </param>
        /// <param name="paging"></param>
        /// <param name="queryParameter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int oprator,[FromQuery]PagingModel paging, [FromQuery]SysMenusRequestModel queryParameter)
        {
            var apiResult = new ApiResultModel(ErrorCodeType.Success);
            switch (paging.Oprator)
            {
                case 1:
                    {
                        //获取不分页数据集合
                        var data = await sysMenusService.GetModelsAsync(queryParameter);
                        apiResult.Result = data;
                        return Ok(apiResult);
                    }
                default:
                    {
                        var data = await sysMenusService.GetModelsAsync(paging, queryParameter);
                        apiResult.Result = new
                        {
                            paging,
                            data
                        };
                        return Ok(apiResult);
                    }
            }
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var _data = await sysMenusService.GetModelAsync(id);
            var apiResult = new ApiResultModel(ErrorCodeType.Success, _data);
            return Ok(apiResult);
        }

        /// <summary>
        /// 新增一条
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SysMenusRequestModel parameter)
        {
            var apiResult = new ApiResultModel(ErrorCodeType.Success);
            var model = new SysMenusModel();
            parameter.CloneTo(model);
            if (await sysMenusService.CreateAsync(model))
            {
                return Created($"{route}/{parameter.menu_id}", apiResult);
            }
            apiResult.Code = ErrorCodeType.PostError;
            return BadRequest(apiResult);
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="parameter">修改的字段</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]SysMenusModel parameter)
        {
            var apiResult = new ApiResultModel(ErrorCodeType.Success);
            parameter.menu_id = id;
            //var model = new SysMenusModel();
            //parameter.CloneTo(model);
            var c = await sysMenusService.UpdateModelAsync(parameter);
            if (c > 0)
            {
                return Ok(apiResult);
            }
            apiResult.Code = ErrorCodeType.PutError;
            return BadRequest(apiResult);
        }

        /// <summary>
        /// 局部更新
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPatch()]
        public async Task<IActionResult> Patch([FromBody]SysMenusModel parameter)
        {
            var apiResult = new ApiResultModel(ErrorCodeType.Success);
            //var model = new SysMenusModel();
            //parameter.CloneTo(model);
            var c = await sysMenusService.UpdateModelAsync(parameter);
            if (c > 0)
            {
                return Ok(apiResult);
            }
            apiResult.Code = ErrorCodeType.PutError;
            return BadRequest(apiResult);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var apiResult = new ApiResultModel(ErrorCodeType.Success);
            if (string.IsNullOrEmpty(id))
            {
                apiResult.Code = ErrorCodeType.ParamsError;
                return BadRequest(apiResult);
            }
            
            var c = await sysMenusService.DeleteAsync(new string[] { id });
            if (c > 0)
            {
                return Ok(apiResult);
            }
            apiResult.Code = ErrorCodeType.DeleteError;
            return BadRequest(apiResult);
        }

        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]string[] idList)
        {
            var apiResult = new ApiResultModel(ErrorCodeType.Success);
            if (idList == null || idList.Length < 1)
            {
                apiResult.Code = ErrorCodeType.ParamsError;
                return BadRequest(apiResult);
            }
            var c = await sysMenusService.DeleteAsync(idList);
            if (c > 0)
            {
                return Ok(apiResult);
            }
            apiResult.Code = ErrorCodeType.DeleteError;
            return BadRequest(apiResult);
        }
    }
}