using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.App.Models.Enums
{
    public enum EnumTableOptions
    {
        None=0,
        指定表生成=1,
    }

    public enum EnumDbType
    {
        SqlServer = 0,
        MySql = 1,
    }
}
