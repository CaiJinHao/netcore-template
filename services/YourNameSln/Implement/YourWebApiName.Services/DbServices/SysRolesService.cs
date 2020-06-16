

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
    public class SysRolesService : ISysRolesService
    {
        /// <summary>
        /// 服务 DbServices sys_roles
        /// </summary>
        public SysRolesService()
        {
        
        }        
    
        public ISysRolesRepository sysRolesRepository { get; set; }


        public async Task<bool> CreateAsync(SysRolesModel model)
        {
            return await sysRolesRepository.CreateAsync(model);
        }

        public async Task<long> DeleteAsync(string[] id)
        {
            return await sysRolesRepository.DeleteAsync(id);
        }

        public Task<long> DeleteAsync(SysRolesModel model, string[] notInFields = null)
        {
            throw new NotImplementedException();
        }

        public async Task<SysRolesModel> GetModelAsync(string id, IEnumerable<string> fields = null)
        {
            return await sysRolesRepository.GetModelAsync(id, fields);
        }

        public async Task<IEnumerable<SysRolesResponeModel>> GetModelsAsync(SysRolesRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            return await sysRolesRepository.GetModelsAsync(queryParameter, fields);
        }

        public async Task<IEnumerable<SysRolesResponeModel>> GetModelsAsync(PagingModel pagingModel, SysRolesRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            return await sysRolesRepository.GetModelsAsync(pagingModel, queryParameter, fields);
        }

        public async Task<long> UpdateModelAsync(string id, SysRolesModel model)
        {
            return await sysRolesRepository.UpdateModelAsync(id, model);
        }
    }
}
