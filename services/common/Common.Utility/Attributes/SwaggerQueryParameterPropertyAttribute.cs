using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Attributes
{
    /// <summary>
    /// 查询参数属性是否显示
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerQueryParameterPropertyAttribute : Attribute
    {
        /// <summary>
        /// true 显示 false 不显示
        /// </summary>
        /// <param name="visible">是否显示字段</param>
        public SwaggerQueryParameterPropertyAttribute(bool _visible)
        {
            Visible = _visible;
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
