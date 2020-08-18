using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Core.DataForm.LogQuery
{
    /// <summary>
    /// 日志查询参数
    /// </summary>
   public class LogQueryForm:QueryBaseFrom
    {
        public string HandleUser { get; set; }
    }
}
