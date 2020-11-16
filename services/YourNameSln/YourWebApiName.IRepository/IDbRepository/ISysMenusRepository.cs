

using System;
using Common.Utility.Models.HttpModels;
using IDataBase.IRepository;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;

namespace YourWebApiName.IRepository.IDbRepository
{
    /// <summary>
    /// 资源 IDbRepository 系统_菜单
    /// </summary>
    public interface ISysMenusRepository : IDbInteraction<SysMenusModel,SysMenusResponeModel,SysMenusRequestModel, string, PagingModel>
    {
    }
}
