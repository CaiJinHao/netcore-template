using Common.Utility.Models.HttpModels;
using IDataBase.IServices;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;

namespace YourWebApiName.IServices.IDbServices
{
    /// <summary>
    /// 服务 IDbServices sys_roles
    /// </summary>
    public interface ISysRolesService : IDbServicesBase<SysRolesModel,SysRolesResponeModel,SysRolesRequestModel, string, PagingModel>
    {
    }
}

