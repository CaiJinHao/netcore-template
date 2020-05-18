using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Attributes
{
    /// <summary>
    /// 请求Body参数属性是否显示
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerBodyParameterPropertyAttribute: Attribute
    {
        public SwaggerBodyParameterPropertyAttribute(bool visible)
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
