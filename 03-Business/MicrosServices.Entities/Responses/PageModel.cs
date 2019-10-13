using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Responses
{
    /// <summary>
    /// 分页信息类
    /// </summary>
    public class PageModel
    {
        /// <summary>
        /// 默认每页10行
        /// </summary>
        public const int DefaultPageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页多少行
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        public PageModel(int pageIndex,int pageSize,int totalCount)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            if (pageSize == 0)
            {
                pageSize = totalCount > pageSize ? totalCount : pageSize;
            }
            this.TotalCount = totalCount;
            this.PageCount = (TotalCount + PageSize - 1) / PageSize;
        }
    }
}
