

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
    public class SysUsersService : ISysUsersService
    {
        /// <summary>
        /// 服务 DbServices 系统_用户
        /// </summary>
        public SysUsersService()
        {
        
        }        
    
        public ISysUsersRepository sysUsersRepository { get; set; }
        //解决依赖循环问题private { get => AutofacHelper.GetScopeService<ISysUsersRepository>(); }


        public async Task<bool> CreateAsync(SysUsersModel model)
        {
            return await sysUsersRepository.CreateAsync(model);
        }

        public async Task<long> DeleteAsync(string[] id)
        {
            return await sysUsersRepository.DeleteAsync(id);
        }

        public async Task<long> DeleteAsync(SysUsersModel model)
        {
            return await sysUsersRepository.DeleteAsync(model);
        }

        public async Task<SysUsersModel> GetModelAsync(string id, IEnumerable<string> fields = null)
        {
            return await sysUsersRepository.GetModelAsync(id, fields);
        }

        public async Task<IEnumerable<SysUsersResponeModel>> GetModelsAsync(SysUsersRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            return await sysUsersRepository.GetModelsAsync(queryParameter, fields);
        }

        public async Task<IEnumerable<SysUsersModel>> GetCurrentModelsAsync(SysUsersRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            return await sysUsersRepository.GetCurrentModelsAsync(queryParameter, fields);
        }

        public async Task<IEnumerable<SysUsersResponeModel>> GetModelsAsync(PagingModel pagingModel, SysUsersRequestModel queryParameter, IEnumerable<string> fields = null)
        {
            return await sysUsersRepository.GetModelsAsync(pagingModel, queryParameter, fields);
        }

        public async Task<long> UpdateAllModelAsync(SysUsersModel model)
        {
            return await sysUsersRepository.UpdateAllModelAsync(model);
        }

        public async Task<long> UpdateModelAsync(SysUsersModel model)
        {
            return await sysUsersRepository.UpdateModelAsync(model);
        }
    }
}
