

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourWebApiName.Models.DbModels
{
    /// <summary>
    /// Db Entity 系统_菜单
    /// </summary>
    [Table("sys_menus")]
    public class SysMenusModel
    {
         /// <summary>
         /// menu_id
         /// </summary>
         [Key]
         [Required]
         [StringLength(50)]
         public string menu_id { get; set; }
         /// <summary>
         /// menu_菜单名称
         /// </summary>
         [Required]
         [StringLength(20)]
         public string menu_name { get; set; }
         /// <summary>
         /// menu_菜单图标
         /// </summary>
         [StringLength(30)]
         public string menu_icon { get; set; }
         /// <summary>
         /// menu_菜单排序
         /// </summary>
         [Required]
         public int menu_sort { get; set; }
         /// <summary>
         /// menu_父级菜单
         /// </summary>
         [StringLength(50)]
         public string menu_parent_id { get; set; }
         /// <summary>
         /// menu_菜单等级
         /// </summary>
         [Required]
         public int menu_grade { get; set; }
         /// <summary>
         /// menu_菜单URL
         /// </summary>
         [Required]
         [StringLength(500)]
         public string menu_url { get; set; }
         /// <summary>
         /// menu_菜单描述
         /// </summary>
         [StringLength(200)]
         public string menu_description { get; set; }
         /// <summary>
         /// menu_菜单是否启用
         /// </summary>
         [Required]
         public int menu_is_enabled { get; set; }
    }
}
