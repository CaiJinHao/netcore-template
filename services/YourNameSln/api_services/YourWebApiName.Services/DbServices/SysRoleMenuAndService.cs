


using YourWebApiName.IRepository.IDbRepository;
using YourWebApiName.IServices.IDbServices;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;
using Common.Utility.Models.App;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourWebApiName.Services.DbServices
{
    public class SysRoleMenuAndService : ISysRoleMenuAndService
    {
        /// <summary>
        /// 服务 DbServices 系统_角色菜单权限
        /// </summary>
        public SysRoleMenuAndService()
        {
        
        }        
    
        public ISysRoleMenuAndRepository sysRoleMenuAndRepository { get; set; }


        public async Task<bool> CreateAsync(SysRoleMenuAndModel model)
        {
            return await sysRoleMenuAndRepository.CreateAsync(model);
        }

        public async Task<long> DeleteAsync(string[] id)
        {
            return await sysRoleMenuAndRepository.DeleteAsync(id);
        }

        public async Task<SysRoleMenuAndModel> GetModelAsync(string id)
        {
            return await sysRoleMenuAndRepository.GetModelAsync(id);
        }

        public async Task<IEnumerable<SysRoleMenuAndResponeModel>> GetModelsAsync(SysRoleMenuAndRequestModel queryParameter)
        {
            return await sysRoleMenuAndRepository.GetModelsAsync(queryParameter);
        }

        public async Task<IEnumerable<SysRoleMenuAndResponeModel>> GetModelsAsync(PagingModel pagingModel, SysRoleMenuAndRequestModel queryParameter)
        {
            return await sysRoleMenuAndRepository.GetModelsAsync(pagingModel, queryParameter);
        }

        public async Task<long> UpdateModelAsync(string id, SysRoleMenuAndModel model)
        {
            return await sysRoleMenuAndRepository.UpdateModelAsync(id, model);
        }
    }
}
