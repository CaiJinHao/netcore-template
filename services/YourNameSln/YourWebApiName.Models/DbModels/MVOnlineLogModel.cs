

using Common.Utility.JsonConverter;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace YourWebApiName.Models.DbModels
{
    /// <summary>
    /// Db Entity TB_MV_OnlineLog
    /// </summary>
    [Table("TB_MV_OnlineLog")]
    public class MVOnlineLogModel
    {
         /// <summary>
         /// 编号
         /// </summary>
         [Key]
         public decimal No { get; set; }
         /// <summary>
         /// 直属库编码
         /// </summary>
         public string OrgCode { get; set; }
         /// <summary>
         /// CameraIndexCode
         /// </summary>
         [Required]
         public string CameraIndexCode { get; set; }
        /// <summary>
        /// DVR编号
        /// </summary>
        public int DVRNo { get; set; }
         /// <summary>
         /// 通道编号
         /// </summary>
         public int ChannelNo { get; set; }
         /// <summary>
         /// 人员编号
         /// </summary>
         public string UserCode { get; set; }
         /// <summary>
         /// 人员名称
         /// </summary>
         public string UserName { get; set; }
         /// <summary>
         /// 所属单位编号
         /// </summary>
         public string UnitCode { get; set; }
         /// <summary>
         /// 所属单位名称
         /// </summary>
         public string UnitName { get; set; }
         /// <summary>
         /// 开始日期
         /// </summary>
         [JsonConverter(typeof(JsonDateTimeConverter))]
         public DateTime? LoginDate { get; set; }
         /// <summary>
         /// 结束日期
         /// </summary>
         [JsonConverter(typeof(JsonDateTimeConverter))]
         public DateTime? LogoutDate { get; set; }
         /// <summary>
         /// 监控位置类型：库区，仓内
         /// </summary>
         public string CamerType { get; set; }
         /// <summary>
         /// 监控位置类型名称
         /// </summary>
         public string CamerTypeName { get; set; }
         /// <summary>
         /// 备注
         /// </summary>
         public string Memo { get; set; }
         /// <summary>
         /// 同步时间
         /// </summary>
         [JsonConverter(typeof(JsonDateTimeConverter))]
         public DateTime? SynTime { get; set; }
    }
}
