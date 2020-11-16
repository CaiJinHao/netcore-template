

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
    public class SysMenusService : ISysMenusService
    {
        /// <summary>
        /// 服务 DbServices 系统_菜单
        /// </summary>
        public SysMenusService()
        {
        
        }        
    
        public ISysMenusRepository sysMenusRepository { get; set; }
        //解决依赖循环问题private { get => AutofacHelper.GetScopeService<ISysMenusRepository>(); }


        public async Task<bool> CreateAsync(SysMenusModel model)
        {
            return await sysMenusRepository.CreateAsync(model);
        }

        public async Task<long> DeleteAsync(string[] id)
        {
            return await sysMenusRepository.DeleteAsync(id);
        }

        public async Task<long> DeleteAsync(SysMenusModel model)
        {
            return await sysMenusRepository.DeleteAsync(model);
        }

        public async Task<SysMenusModel> GetModelAsync(string id, IEnumerable<string> fields = null)
        {
            return await sysMenusRepository.GetModelAsync(id, fields);
        }

        public async Task<IEnumerable<SysMenusResponeModel>> GetModelsAsync(SysMenusRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            return await sysMenusRepository.GetModelsAsync(queryParameter, fields);
        }

        public async Task<IEnumerable<SysMenusModel>> GetCurrentModelsAsync(SysMenusRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            return await sysMenusRepository.GetCurrentModelsAsync(queryParameter, fields);
        }

        public async Task<IEnumerable<SysMenusResponeModel>> GetModelsAsync(PagingModel pagingModel, SysMenusRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            return await sysMenusRepository.GetModelsAsync(pagingModel, queryParameter, fields);
        }

        public async Task<long> UpdateAllModelAsync(SysMenusModel model)
        {
            return await sysMenusRepository.UpdateAllModelAsync(model);
        }

        public async Task<long> UpdateModelAsync(SysMenusModel model)
        {
            return await sysMenusRepository.UpdateModelAsync(model);
        }
    }
}
