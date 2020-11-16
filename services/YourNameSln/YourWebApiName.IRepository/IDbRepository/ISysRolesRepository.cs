

using System;
using Common.Utility.Models.HttpModels;
using IDataBase.IRepository;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;

namespace YourWebApiName.IRepository.IDbRepository
{
    /// <summary>
    /// 资源 IDbRepository 系统_角色
    /// </summary>
    public interface ISysRolesRepository : IDbInteraction<SysRolesModel,SysRolesResponeModel,SysRolesRequestModel, string, PagingModel>
    {
    }
}
