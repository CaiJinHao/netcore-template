

using System;
using Common.Utility.Models.HttpModels;
using IDataBase.IRepository;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;

namespace YourWebApiName.IRepository.IDbRepository
{
    /// <summary>
    /// 资源 IDbRepository 系统_用户
    /// </summary>
    public interface ISysUsersRepository : IDbInteraction<SysUsersModel,SysUsersResponeModel,SysUsersRequestModel, string, PagingModel>
    {
    }
}
