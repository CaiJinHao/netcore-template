

using System;
using Common.Utility.Models.HttpModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourWebApiName.IRepository.IDbRepository;
using YourWebApiName.IServices.IDbServices;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;

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
        //解决依赖循环问题private { get => AutofacHelper.GetScopeService<ISysRoleMenuAndRepository>(); }


        public async Task<bool> CreateAsync(SysRoleMenuAndModel model)
        {
            return await sysRoleMenuAndRepository.CreateAsync(model);
        }

        public async Task<long> DeleteAsync(string[] id)
        {
            return await sysRoleMenuAndRepository.DeleteAsync(id);
        }

        public async Task<long> DeleteAsync(SysRoleMenuAndModel model)
        {
            return await sysRoleMenuAndRepository.DeleteAsync(model);
        }

        public async Task<SysRoleMenuAndModel> GetModelAsync(string id, IEnumerable<string> fields = null)
        {
            return await sysRoleMenuAndRepository.GetModelAsync(id, fields);
        }

        public async Task<IEnumerable<SysRoleMenuAndResponeModel>> GetModelsAsync(SysRoleMenuAndRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            return await sysRoleMenuAndRepository.GetModelsAsync(queryParameter, fields);
        }

        public async Task<IEnumerable<SysRoleMenuAndModel>> GetCurrentModelsAsync(SysRoleMenuAndRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            return await sysRoleMenuAndRepository.GetCurrentModelsAsync(queryParameter, fields);
        }

        public async Task<IEnumerable<SysRoleMenuAndResponeModel>> GetModelsAsync(PagingModel pagingModel, SysRoleMenuAndRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            return await sysRoleMenuAndRepository.GetModelsAsync(pagingModel, queryParameter, fields);
        }

        public async Task<long> UpdateAllModelAsync(SysRoleMenuAndModel model)
        {
            return await sysRoleMenuAndRepository.UpdateAllModelAsync(model);
        }

        public async Task<long> UpdateModelAsync(SysRoleMenuAndModel model)
        {
            return await sysRoleMenuAndRepository.UpdateModelAsync(model);
        }
    }
}
