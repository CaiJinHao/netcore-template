using DataBase.IDataBase;

namespace CodeGenerator.App.Models
{
    public class StaticConfig
    {
        /// <summary>
        /// 项目根路径
        /// </summary>
        public static string ContentRootPath { get; set; }
        public static AppSettings AppSettings { get; set; }
        /// <summary>
        /// 尽量少的出现 更换数据上下文时比较简单
        /// </summary>
        public static ISqlDbContext DbContext { get; set; }
    }
}
