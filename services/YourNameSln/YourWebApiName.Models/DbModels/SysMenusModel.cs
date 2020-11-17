

using Common.Utility.JsonConverter;
using Common.Utility.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
         public string menu_id { get; set; }
        /// <summary>
        /// menu_菜单名称
        /// </summary>
        [Required]
         public string menu_name { get; set; }
         /// <summary>
         /// menu_菜单图标
         /// </summary>
         public string menu_icon { get; set; }
         /// <summary>
         /// menu_菜单排序
         /// </summary>
         public int menu_sort { get; set; }
         /// <summary>
         /// menu_父级菜单
         /// </summary>
         public string menu_parent_id { get; set; }
         /// <summary>
         /// menu_菜单等级
         /// </summary>
         public int menu_grade { get; set; }
         /// <summary>
         /// menu_菜单URL
         /// </summary>
         public string menu_url { get; set; }
         /// <summary>
         /// menu_菜单描述
         /// </summary>
         public string menu_description { get; set; }
         /// <summary>
         /// menu_菜单是否启用
         /// </summary>
         public EnumIsNot menu_is_enabled { get; set; }
    }
}
