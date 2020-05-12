using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.UiModels
{
    public class LayoutMenusModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string icon { get; set; }
        /// <summary>
        /// 数组类型
        /// </summary>
        public dynamic children { get; set; }
        public bool @checked { get; set; }
        public EnumIsNot is_module { get; set; }
        public EnumIsNot is_leaf { get; set; }
        public string parent_id { get; set; }
        public int sort { get; set; }
    }
}
