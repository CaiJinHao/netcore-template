

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YourWebApiName.Models.DbModels;

namespace YourWebApiName.Models.RequestModels
{
    /// <summary>
    /// Request Entity 系统_角色菜单权限
    /// </summary>
    public class SysRoleMenuAndRequestModel : SysRoleMenuAndModel
    {
        [Required]
        public string[] menu_id_list { get; set; }
    }
}
