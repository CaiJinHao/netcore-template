

using DataBase;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;

namespace YourWebApiName.IServices.IDbServices
{
    /// <summary>
    /// 服务 IDbServices 系统_角色菜单权限
    /// </summary>
    public interface ISysRoleMenuAndService : IDbServicesBase<SysRoleMenuAndModel,SysRoleMenuAndResponeModel,SysRoleMenuAndRequestModel, string>
    {
    }
}

