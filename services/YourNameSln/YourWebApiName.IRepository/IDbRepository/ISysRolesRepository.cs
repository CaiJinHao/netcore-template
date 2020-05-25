

using Common.Utility.Models.HttpModels;
using IDataBase.IRepository;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;

namespace YourWebApiName.IRepository.IDbRepository
{
    /// <summary>
    /// 资源 IDbRepository sys_roles
    /// </summary>
    public interface ISysRolesRepository : IDbInteraction<SysRolesModel,SysRolesResponeModel,SysRolesRequestModel, string, PagingModel>
    {
    }
}
