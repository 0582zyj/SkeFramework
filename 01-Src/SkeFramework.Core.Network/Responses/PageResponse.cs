using SkeFramework.Core.Network.DataUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Network.Responses
{
    public class PageResponse<T> 
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public PageModel page { get; set; }
        /// <summary>
        /// 结果数据
        /// </summary>
        public List<T> dataList { get; set; }
    }
}
