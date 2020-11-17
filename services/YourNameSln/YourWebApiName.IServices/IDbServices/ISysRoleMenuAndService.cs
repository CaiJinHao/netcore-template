

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Utility.Models.HttpModels;
using Common.Utility.Models.UiModels;
using IDataBase.IServices;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;

namespace YourWebApiName.IServices.IDbServices
{
    /// <summary>
    /// 服务 IDbServices 系统_角色菜单权限
    /// </summary>
    public interface ISysRoleMenuAndService : IDbServicesBase<SysRoleMenuAndModel,SysRoleMenuAndResponeModel,SysRoleMenuAndRequestModel, string, PagingModel>
    {
        Task<IEnumerable<LayoutMenusModel>> GetLayoutMenusAsync(string role_id);
    }
}

