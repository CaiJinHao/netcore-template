using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Utility.Attributes;
using Common.Utility.JsonConverter;
using System.Text.Json.Serialization;

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
         [SwaggerQueryParameterProperty(false)]
         [SwaggerBodyParameterProperty(true)]
         public string user_id { get; set; }
         /// <summary>
         /// role_id
         /// </summary>
         [Required]
         [SwaggerQueryParameterProperty(false)]
         [SwaggerBodyParameterProperty(true)]
         public string role_id { get; set; }
         /// <summary>
         /// user_账号
         /// </summary>
         [Required]
         public string user_account { get; set; }
         /// <summary>
         /// user_密码
         /// </summary>
         [Required]
         public string user_pwd { get; set; }
         /// <summary>
         /// user_是否启用
         /// </summary>
         public int user_is_enable { get; set; }
         /// <summary>
         /// user_手机号
         /// </summary>
         public string user_phone { get; set; }
         /// <summary>
         /// user_邮箱
         /// </summary>
         public string user_email { get; set; }
         /// <summary>
         /// user_创建时间
         /// </summary>
         [JsonConverter(typeof(JsonDateTimeConverter))]
         public DateTime user_time { get; set; }
         /// <summary>
         /// user_头像
         /// </summary>
         [Required]
         public string user_icon { get; set; }
         /// <summary>
         /// user_名称
         /// </summary>
         [Required]
         public string user_name { get; set; }
         /// <summary>
         /// user_性别
         /// </summary>
         public int user_sex { get; set; }
    }
}
