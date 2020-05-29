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
        /// 服务 DbServices 系统_角色
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
            fields = new List<string>() {
               "role_id",
               "role_name"
            };
            return await sysRolesRepository.GetModelsAsync(pagingModel, queryParameter, fields);
        }

        public async Task<long> UpdateModelAsync(string id, SysRolesModel model)
        {
            return await sysRolesRepository.UpdateModelAsync(id, model);
        }
    }
}
