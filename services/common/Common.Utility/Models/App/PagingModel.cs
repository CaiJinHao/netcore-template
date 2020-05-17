using Common.Utility.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Models.App
{
    /// <summary>
    /// 分页器
    /// </summary>
    public class PagingModel
    {
        private int page = 1;
        /// <summary>
        /// 当前要访问的页数
        /// </summary>
        public int Page { get => page; set => page = value; }

        private int pageSize = 10;
        /// <summary>
        /// 每页的数量 Limit
        /// </summary>
        public int PageSize { get => pageSize; set => pageSize = value; }
        

        /// <summary>
        /// 开始数据行索引
        /// </summary>
        /// <returns></returns>
        public int StartIndex()
        {
            return PageSize * (Page - 1);
        }

        #region 当Page=1时返回

        /// <summary>
        /// 总页数
        /// </summary>
        [SwaggerQueryParameterProperty(false)]
        public int TotalPages { get; set; }

        private long _totalCount = 0;
        /// <summary>
        /// 数据总数量
        /// </summary>
        [SwaggerQueryParameterProperty(false)]
        public long TotalCount
        {
            get { return _totalCount; }
            set
            {
                _totalCount = value;
                TotalPages = (int)Math.Ceiling((double)value / PageSize);
            }
        }

        #endregion

        /// <summary>
        /// 前端排序字段
        /// </summary>
        public string OrderByFiled { get; set; }
    }
}
