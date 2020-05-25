using Common.Utility.Extension;
using Common.Utility.Models;
using Common.Utility.Models.HttpModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YourWebApiName.IServices.IDbServices;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;

namespace YourWebApiName.ApiServices.RestApi.v1.sys
{
    /// <summary>
    /// 系统_角色
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/sysroles")]
    [ApiController]
    public class SysRolesController : ControllerBase
    {
        private string route = "api/v1/sysroles";
        /// <summary>
        /// 服务
        /// </summary>
        public ISysRolesService sysRolesService { get; set; }

        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="queryParameter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PagingModel paging, [FromQuery]SysRolesRequestModel queryParameter)
        {
            var apiResult = new ApiResultModel(ErrorCodeType.Success);
            switch (paging.Oprator)
            {
                default:
                    {
                        var data = await sysRolesService.GetModelsAsync(paging, queryParameter);
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
            var _data = await sysRolesService.GetModelAsync(id);
            var apiResult = new ApiResultModel(ErrorCodeType.Success, _data);
            return Ok(apiResult);
        }

        /// <summary>
        /// 新增一条
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SysRolesRequestModel parameter)
        {
            var apiResult = new ApiResultModel(ErrorCodeType.Success);
            var model = new SysRolesModel();
            parameter.CloneTo(model);
            if (await sysRolesService.CreateAsync(model))
            {
                return Created($"{route}/{parameter.role_id}", apiResult);
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
        public async Task<IActionResult> Put(string id, [FromBody]SysRolesRequestModel parameter)
        {
            var apiResult = new ApiResultModel(ErrorCodeType.Success);
            var model = new SysRolesModel();
            parameter.CloneTo(model);
            var c = await sysRolesService.UpdateModelAsync(id, model);
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
            var c = await sysRolesService.DeleteAsync(new string[] { id });
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
            var c = await sysRolesService.DeleteAsync(idList);
            if (c > 0)
            {
                return Ok(apiResult);
            }
            apiResult.Code = ErrorCodeType.DeleteError;
            return BadRequest(apiResult);
        }
    }
}