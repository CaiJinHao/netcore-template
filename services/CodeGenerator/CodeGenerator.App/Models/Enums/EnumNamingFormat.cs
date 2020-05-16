using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.App.Models.Enums
{
    /// <summary>
    /// 表、字段命名格式 PowerDesign格式
    /// </summary>
    public enum EnumNamingFormat
    {
        /// <summary>
        /// 如：Sys_User,Su_UserName
        /// appsettings中要命名为此格式
        /// </summary>
        大驼峰Pascal_符号分割 = 1,
        /// <summary>
        /// 如：sys_user,su_user_name
        /// appsettings中要命名为此格式
        /// </summary>
        small_camel_符号分割 = 2,
    }
}
