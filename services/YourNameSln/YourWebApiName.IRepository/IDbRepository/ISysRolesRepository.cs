

using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;
using DataBase.IDataBase;

namespace YourWebApiName.IRepository.IDbRepository
{
    /// <summary>
    /// 资源 IDbRepository 系统_角色
    /// </summary>
    public interface ISysRolesRepository : IDbInteraction<SysRolesModel,SysRolesResponeModel,SysRolesRequestModel, string>
    {
    }
}
