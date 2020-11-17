

using System;
using System.Collections.Generic;
using System.Text;
using YourWebApiName.Models.DbModels;

namespace YourWebApiName.Models.ResponeModels
{
    /// <summary>
    /// Respone Entity 系统_角色
    /// </summary>
    public class SysRolesResponeModel : SysRolesModel
    {
       public string ParentName { get; set; }
    }
}
