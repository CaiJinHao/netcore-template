

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourWebApiName.Models.DbModels
{
    /// <summary>
    /// Db Entity 系统_用户
    /// </summary>
    [Table("sys_users")]
    public class SysUsersModel
    {
         /// <summary>
         /// user_id
         /// </summary>
         [Key]
         [Required]
         [StringLength(50)]
         public string user_id { get; set; }
         /// <summary>
         /// role_id
         /// </summary>
         [Required]
         [StringLength(50)]
         public string role_id { get; set; }
         /// <summary>
         /// user_账号
         /// </summary>
         [Required]
         [StringLength(30)]
         public string user_account { get; set; }
         /// <summary>
         /// user_密码
         /// </summary>
         [Required]
         [StringLength(30)]
         public string user_pwd { get; set; }
         /// <summary>
         /// user_是否启用
         /// </summary>
         [Required]
         public int user_is_enable { get; set; }
         /// <summary>
         /// user_手机号
         /// </summary>
         [StringLength(11)]
         public string user_phone { get; set; }
         /// <summary>
         /// user_邮箱
         /// </summary>
         [StringLength(30)]
         public string user_email { get; set; }
         /// <summary>
         /// user_创建时间
         /// </summary>
         [Required]
         public DateTime user_time { get; set; }
         /// <summary>
         /// user_头像
         /// </summary>
         [Required]
         [StringLength(500)]
         public string user_icon { get; set; }
         /// <summary>
         /// user_名称
         /// </summary>
         [Required]
         [StringLength(50)]
         public string user_name { get; set; }
         /// <summary>
         /// user_性别
         /// </summary>
         [Required]
         public int user_sex { get; set; }
    }
}
