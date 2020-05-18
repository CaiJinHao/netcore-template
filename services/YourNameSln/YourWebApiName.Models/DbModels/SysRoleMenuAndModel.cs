

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
    /// Db Entity 系统_角色菜单权限
    /// </summary>
    [Table("sys_role_menu_and")]
    public class SysRoleMenuAndModel
    {
         /// <summary>
         /// rma_id
         /// </summary>
         [Key]
         [SwaggerQueryParameterProperty(false)]
         [SwaggerBodyParameterProperty(true)]
         public string rma_id { get; set; }
         /// <summary>
         /// menu_id
         /// </summary>
         [Required]
         [SwaggerQueryParameterProperty(false)]
         [SwaggerBodyParameterProperty(true)]
         public string menu_id { get; set; }
         /// <summary>
         /// role_id
         /// </summary>
         [Required]
         [SwaggerQueryParameterProperty(false)]
         [SwaggerBodyParameterProperty(true)]
         public string role_id { get; set; }
         /// <summary>
         /// rma_time
         /// </summary>
         [JsonConverter(typeof(JsonDateTimeConverter))]
         public DateTime rma_time { get; set; }
    }
}
