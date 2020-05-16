using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.UiModels
{
    public class MenuTreeModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string value { get; set; }
        public IEnumerable<MenuTreeModel> data { get; set; }
        public bool @checked { get; set; }
        public bool disabled { get; set; }
        public string parent_id { get; set; }
        public EnumIsNot is_module { get; set; }
        public EnumIsNot is_leaf { get; set; }

    }
}
