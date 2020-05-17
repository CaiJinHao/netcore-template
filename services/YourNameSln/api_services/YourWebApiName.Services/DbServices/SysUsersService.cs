


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
    public class SysUsersService : ISysUsersService
    {
        /// <summary>
        /// 服务 DbServices 系统_用户
        /// </summary>
        public SysUsersService()
        {
        
        }        
    
        public ISysUsersRepository sysUsersRepository { get; set; }


        public async Task<bool> CreateAsync(SysUsersModel model)
        {
            return await sysUsersRepository.CreateAsync(model);
        }

        public async Task<long> DeleteAsync(string[] id)
        {
            return await sysUsersRepository.DeleteAsync(id);
        }

        public async Task<SysUsersModel> GetModelAsync(string id)
        {
            return await sysUsersRepository.GetModelAsync(id);
        }

        public async Task<IEnumerable<SysUsersResponeModel>> GetModelsAsync(SysUsersRequestModel queryParameter)
        {
            return await sysUsersRepository.GetModelsAsync(queryParameter);
        }

        public async Task<IEnumerable<SysUsersResponeModel>> GetModelsAsync(PagingModel pagingModel, SysUsersRequestModel queryParameter)
        {
            return await sysUsersRepository.GetModelsAsync(pagingModel, queryParameter);
        }

        public async Task<long> UpdateModelAsync(string id, SysUsersModel model)
        {
            return await sysUsersRepository.UpdateModelAsync(id, model);
        }
    }
}
