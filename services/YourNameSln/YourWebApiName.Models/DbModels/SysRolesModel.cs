

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourWebApiName.Models.DbModels
{
    /// <summary>
    /// Db Entity 系统_角色
    /// </summary>
    [Table("sys_roles")]
    public class SysRolesModel
    {
         /// <summary>
         /// role_id
         /// </summary>
         [Key]
         [Required]
         [StringLength(50)]
         public string role_id { get; set; }
         /// <summary>
         /// role_名称
         /// </summary>
         [Required]
         [StringLength(30)]
         public string role_name { get; set; }
         /// <summary>
         /// role_附注
         /// </summary>
         [StringLength(200)]
         public string role_remarks { get; set; }
         /// <summary>
         /// role_父级角色
         /// </summary>
         [StringLength(50)]
         public string role_parent_role { get; set; }
         /// <summary>
         /// role_排序
         /// </summary>
         [Required]
         public int role_sort { get; set; }
         /// <summary>
         /// role_角色等级
         /// </summary>
         [Required]
         public int role_grade { get; set; }
         /// <summary>
         /// role_角色是否启用
         /// </summary>
         [Required]
         public int role_is_enable { get; set; }
    }
}
