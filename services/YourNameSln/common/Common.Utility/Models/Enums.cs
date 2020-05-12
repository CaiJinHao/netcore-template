using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Common.Utility.Models
{
    public enum EnumIsNot
    {
        None = 0,
        [Description("是")]
        Yes=1,
        [Description("否")]
        No=2
    }
}
