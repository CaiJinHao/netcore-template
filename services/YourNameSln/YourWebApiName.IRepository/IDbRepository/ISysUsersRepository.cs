

using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;
using DataBase.IDataBase;

namespace YourWebApiName.IRepository.IDbRepository
{
    /// <summary>
    /// 资源 IDbRepository 系统_用户
    /// </summary>
    public interface ISysUsersRepository : IDbInteraction<SysUsersModel,SysUsersResponeModel,SysUsersRequestModel, string>
    {
    }
}
