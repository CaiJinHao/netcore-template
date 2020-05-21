using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.App.DbModels
{
    /// <summary>
    /// Models命名空间实体
    /// </summary>
    public class ModelsFileModel: TablesModel
    {
        /// <summary>
        /// API 版本
        /// </summary>
        public string api_version { get; set; }
        /// <summary>
        /// API 控制器的命名空间
        /// </summary>
        public string api_controller_name_space { get; set; }

        /// <summary>
        /// 第一级命名空间
        /// </summary>
        public string name_space { get; set; }
        /// <summary>
        /// 重命名的表名称
        /// </summary>
        public string table_name_rename { get; set; }
        /// <summary>
        /// pascal 表名 开头大写
        /// </summary>
        public string table_name_pascal { get; set; }
        /// <summary>
        /// camel 表名 开头小写
        /// </summary>
        public string table_name_camel { get; set; }
        /// <summary>
        /// 小写
        /// </summary>
        public string table_name_lower { get; set; }
        /// <summary>
        /// 主键名称
        /// </summary>
        public string primary_key_name { get; set; }
        /// <summary>
        /// 主键类型
        /// </summary>
        public string primary_key_data_type { get; set; }
        public IEnumerable<ColumnsModel> columns { get; set; }
    }
}
