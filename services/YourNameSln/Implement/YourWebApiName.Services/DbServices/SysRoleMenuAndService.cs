

using System;
using Common.Utility.Models.HttpModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourWebApiName.IRepository.IDbRepository;
using YourWebApiName.IServices.IDbServices;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;
using YourWebApiName.Models.ResponeModels;
using Common.Utility.Autofac;
using Common.Utility.Models.UiModels;
using System.Linq;
using Common.Utility.Models.Config;

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
        private ISysMenusService sysMenusService { get => AutofacHelper.GetService<ISysMenusService>(); }


        public async Task<bool> CreateAsync(SysRoleMenuAndModel model)
        {
            return await sysRoleMenuAndRepository.CreateAsync(model);
        }

        public async Task<bool> CreateAsync(SysRoleMenuAndModel[] model)
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

        public async Task<IEnumerable<LayoutMenusModel>> GetLayoutMenusAsync(string role_id)
        {
            var menuModels = await sysMenusService.GetCurrentModelsAsync(new SysMenusRequestModel()
            {
                menu_is_enabled = Common.Utility.Models.EnumIsNot.Yes
            });
            //获取有权限的菜单
            var auth_menus_id = new List<string>();
            if (role_id.Equals(StaticConfig.SuperadminRoleId))
            {
                auth_menus_id = menuModels.Select(m => m.menu_id).ToList();
            }
            else
            {
                var auth_menus = await GetModelsAsync(new SysRoleMenuAndRequestModel()
                {
                    role_id = role_id
                });
                auth_menus_id = auth_menus.Select(m => m.menu_id).ToList();
            }
            //去除没有权限的菜单
            var menu_list = menuModels.Where(m => auth_menus_id.Contains(m.menu_id))
                .Select(a=>new LayoutMenusModel()
                {
                    id = a.menu_id,
                    icon = a.menu_icon,
                    menu_grade=a.menu_grade,
                    parent_id = a.menu_parent_id,
                    sort = a.menu_sort,
                    title = a.menu_name,
                    url = a.menu_url
                });

            //获取子节点菜单
            async Task<IEnumerable<LayoutMenusModel>> GetChildMenus(string menu_id)
            {
                //给模块节点赋值子节点
                var child_list = menu_list.Where(ch => ch.parent_id == menu_id).ToList();
                //判断子节点是否为叶子节点
                foreach (var menu_leaf in child_list)
                {
                    if (menu_leaf.parent_id.Length != 0)
                    {
                        menu_leaf.children = await GetChildMenus(menu_leaf.id);
                    }
                }
                return child_list;
            }

            //寻找根级节点
            var rootNodes = menu_list.Where(a => a.parent_id.Length == 0).ToList();
            foreach (var item in rootNodes)
            {
                item.children = await GetChildMenus(item.id);
            }
            return rootNodes;
        }

        public async Task<IEnumerable<MenuTreeModel>> GetMenuTreeAsync(SysRoleMenuAndRequestModel queryParameter)
        {
            //获取所有菜单
            var menuModels = await sysMenusService.GetCurrentModelsAsync(new SysMenusRequestModel()
            {
                menu_is_enabled = Common.Utility.Models.EnumIsNot.Yes
            });
            //获取有权限的菜单
            var auth_menus_id = new List<string>();
            if (queryParameter.role_id != null)
            {
                if (queryParameter.role_id.Equals(StaticConfig.SuperadminRoleId))
                {
                    auth_menus_id = menuModels.Select(m => m.menu_id).ToList();
                }
                else
                {
                    var auth_menus = await GetModelsAsync(queryParameter);
                    auth_menus_id = auth_menus.Select(m => m.menu_id).ToList();
                }
            }

            var menu_list = menuModels.Select(a => new MenuTreeModel()
            {
                id = a.menu_id,
                parent_id = a.menu_parent_id,
                title = a.menu_name,
                value = a.menu_id,
                data = new List<MenuTreeModel>(),
                @checked = auth_menus_id.Contains(a.menu_id),
                disabled = false
            });

            //获取子节点菜单
            async Task<IEnumerable<MenuTreeModel>> GetChildMenus(string menu_id)
            {
                //给模块节点赋值子节点
                var child_list = menu_list.Where(ch => ch.parent_id == menu_id).ToList();
                //判断子节点是否为叶子节点
                foreach (var menu_leaf in child_list)
                {
                    if (menu_leaf.parent_id.Length != 0)
                    {
                        menu_leaf.data = await GetChildMenus(menu_leaf.id);
                    }
                }
                return child_list;
            }

            //寻找模块节点
            var rootNodes = menu_list.Where(a => a.parent_id.Length == 0).ToList();
            foreach (var item in rootNodes)
            {
                item.data = await GetChildMenus(item.id);
            }
            return rootNodes;
        }

    }
}
