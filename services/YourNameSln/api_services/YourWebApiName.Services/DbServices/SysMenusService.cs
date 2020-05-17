


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
    public class SysMenusService : ISysMenusService
    {
        /// <summary>
        /// 服务 DbServices 系统_菜单
        /// </summary>
        public SysMenusService()
        {
        
        }        
    
        public ISysMenusRepository sysMenusRepository { get; set; }


        public async Task<bool> CreateAsync(SysMenusModel model)
        {
            return await sysMenusRepository.CreateAsync(model);
        }

        public async Task<long> DeleteAsync(string[] id)
        {
            return await sysMenusRepository.DeleteAsync(id);
        }

        public async Task<SysMenusModel> GetModelAsync(string id)
        {
            return await sysMenusRepository.GetModelAsync(id);
        }

        public async Task<IEnumerable<SysMenusResponeModel>> GetModelsAsync(SysMenusRequestModel queryParameter)
        {
            return await sysMenusRepository.GetModelsAsync(queryParameter);
        }

        public async Task<IEnumerable<SysMenusResponeModel>> GetModelsAsync(PagingModel pagingModel, SysMenusRequestModel queryParameter)
        {
            return await sysMenusRepository.GetModelsAsync(pagingModel, queryParameter);
        }

        public async Task<long> UpdateModelAsync(string id, SysMenusModel model)
        {
            return await sysMenusRepository.UpdateModelAsync(id, model);
        }
    }
}
