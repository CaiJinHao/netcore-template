

using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;
using DataBase.IDataBase;

namespace YourWebApiName.IRepository.IDbRepository
{
    /// <summary>
    /// 资源 IDbRepository 系统_菜单
    /// </summary>
    public interface ISysMenusRepository : IDbInteraction<SysMenusModel,SysMenusResponeModel,SysMenusRequestModel, string>
    {
    }
}
