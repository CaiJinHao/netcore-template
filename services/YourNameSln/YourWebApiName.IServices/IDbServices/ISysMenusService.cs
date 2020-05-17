

using DataBase;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;

namespace YourWebApiName.IServices.IDbServices
{
    /// <summary>
    /// 服务 IDbServices 系统_菜单
    /// </summary>
    public interface ISysMenusService : IDbServicesBase<SysMenusModel,SysMenusResponeModel,SysMenusRequestModel, string>
    {
    }
}

