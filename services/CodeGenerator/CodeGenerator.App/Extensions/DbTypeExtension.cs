using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.App.Extensions
{
    public static class DbTypeExtension
    {
        /// <summary>
        /// 将数据库类型转为C#类型
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static string ConvertType(this string dbType,string is_nullable=null)
        {
            string csType;
            switch (dbType.ToLower())
            {
                case "varchar":
                case "varchar2":
                case "nvarchar":
                case "nvarchar2":
                case "char":
                case "nchar":
                case "text":
                case "longtext":
                case "string":
                    csType = "string";
                    break;

                case "date":
                case "datetime":
                case "smalldatetime":
                case "timestamp":
                    csType = "DateTime";
                    break;

                case "int":
                case "number":
                case "smallint":

                case "integer":
                    csType = "int";
                    break;

                case "bigint":
                    csType = "Int64";
                    break;

                case "float":
                case "numeric":
                case "decimal":
                case "money":
                case "smallmoney":
                case "real":
                case "double":
                    csType = "decimal";
                    break;
                case "tinyint":
                case "bit":
                    csType = "bool";
                    break;

                case "binary":
                case "varbinary":
                case "image":
                case "raw":
                case "long":
                case "long raw":
                case "blob":
                case "bfile":
                    csType = "byte[]";
                    break;

                case "uniqueidentifier":
                    csType = "Guid";
                    break;

                case "xml":
                case "json":
                    csType = "string";
                    break;
                default:
                    csType = "object";
                    break;
            }
            if (!csType.Equals("string") && !string.IsNullOrEmpty(is_nullable) && is_nullable.Equals("YES"))
            {
                csType += "?";//可空类型
            }
            return csType;
        }
    }
}
