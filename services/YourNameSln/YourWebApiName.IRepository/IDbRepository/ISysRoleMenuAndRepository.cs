

using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;
using DataBase.IDataBase;

namespace YourWebApiName.IRepository.IDbRepository
{
    /// <summary>
    /// 资源 IDbRepository 系统_角色菜单权限
    /// </summary>
    public interface ISysRoleMenuAndRepository : IDbInteraction<SysRoleMenuAndModel,SysRoleMenuAndResponeModel,SysRoleMenuAndRequestModel, string>
    {
    }
}
