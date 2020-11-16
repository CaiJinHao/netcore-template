

using System;
using Common.Utility.Models.HttpModels;
using IDataBase.IServices;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;

namespace YourWebApiName.IServices.IDbServices
{
    /// <summary>
    /// 服务 IDbServices 系统_用户
    /// </summary>
    public interface ISysUsersService : IDbServicesBase<SysUsersModel,SysUsersResponeModel,SysUsersRequestModel, string, PagingModel>
    {
    }
}

