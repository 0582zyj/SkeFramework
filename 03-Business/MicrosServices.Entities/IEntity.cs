using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities
{
    /// <summary>
    /// 实体类接口
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        string GetTableName();
        /// <summary>
        /// 获取表关键
        /// </summary>
        /// <returns></returns>
        string GetKey();
    }
}
