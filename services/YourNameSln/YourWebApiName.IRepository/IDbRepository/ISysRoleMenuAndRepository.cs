

using System;
using Common.Utility.Models.HttpModels;
using IDataBase.IRepository;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;

namespace YourWebApiName.IRepository.IDbRepository
{
    /// <summary>
    /// 资源 IDbRepository 系统_角色菜单权限
    /// </summary>
    public interface ISysRoleMenuAndRepository : IDbInteraction<SysRoleMenuAndModel,SysRoleMenuAndResponeModel,SysRoleMenuAndRequestModel, string, PagingModel>
    {
    }
}
