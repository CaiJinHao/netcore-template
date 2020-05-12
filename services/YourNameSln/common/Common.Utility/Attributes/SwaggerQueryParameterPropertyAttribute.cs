using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Attributes
{
    /// <summary>
    /// 查询参数属性处理
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerQueryParameterPropertyAttribute : Attribute
    {
        /// <summary>
        /// true 显示 false 不显示
        /// </summary>
        /// <param name="visible"></param>
        public SwaggerQueryParameterPropertyAttribute(bool visible)
        {
            Visible = visible;
        }
        private bool _visible = true;
        /// <summary>
        /// 是否显示字段 默认显示
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
    }
}
