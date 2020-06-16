----当联合主键的时候
/// <summary>
/// 获取实体
/// </summary>
/// <param name="code">编号</param>
/// <param name="groupNum">组号</param>
/// <returns></returns>
[HttpGet("{code}/{groupNum}")]
public async Task<IActionResult> Get(string code,int groupNum)
{
    var _data = await mVMyMonitorGroupWService.GetCurrentModelsAsync(new MVMyMonitorGroupWRequestModel()
    {
        Code = code,
        GroupNum = groupNum
    });
    var apiResult = new ApiResultModel(ErrorCodeType.Success, _data.FirstOrDefault());
    return Ok(apiResult);
}
/// <summary>
/// 修改实体
/// </summary>
/// <param name="id">主键</param>
/// <param name="parameter">修改的字段</param>
/// <returns></returns>
[HttpPut("{id}")]
public async Task<IActionResult> Put([FromBody]MVMyMonitorGroupWRequestModel parameter)
{
    var apiResult = new ApiResultModel(ErrorCodeType.Success);
    var model = new MVMyMonitorGroupWModel();
    parameter.CloneTo(model);
    //model.Code 实际上没有用
    var c = await mVMyMonitorGroupWService.UpdateModelAsync(model.Code, model);
    if (c > 0)
    {
        return Ok(apiResult);
    }
    apiResult.Code = ErrorCodeType.PutError;
    return BadRequest(apiResult);
}
/// <summary>
/// 根据条件删除多条  需要重写Service层删除方法
/// </summary>
/// <param name="parameter"></param>
/// <returns></returns>
[HttpDelete]
public async Task<IActionResult> Delete([FromBody]MVMyMonitorWModel parameter)
{
    var apiResult = new ApiResultModel(ErrorCodeType.Success);
    var c = await mVMyMonitorWService.DeleteAsync(parameter);
    if (c > 0)
    {
        return Ok(apiResult);
    }
    apiResult.Code = ErrorCodeType.DeleteError;
    return BadRequest(apiResult);
}
public async Task<long> DeleteAsync(MVMyMonitorWModel model)
{
    return await mVMyMonitorWRepository.DeleteAsync(model);
}