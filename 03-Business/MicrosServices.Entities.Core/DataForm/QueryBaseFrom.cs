using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Core.DataForm
{
    /// <summary>
    /// 查询基础参数
    /// </summary>
   public class QueryBaseFrom
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string keywords { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public long queryNo { get; set; }

        public void InitQuery()
        {
            keywords = keywords == null ? "" : keywords;
            queryNo = queryNo == 0 ? -1 : queryNo;
        }
    }
}
