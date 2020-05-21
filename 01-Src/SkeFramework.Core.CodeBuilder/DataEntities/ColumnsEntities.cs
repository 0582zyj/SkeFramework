using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.CodeBuilder.DataEntities
{
    /// <summary>
    /// 列属性
    /// </summary>
    public class ColumnsEntities
    {
        /// <summary>
        /// 序数位置
        /// </summary>
        public int ORDINAL_POSITION { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string COLUMN_NAME { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DATA_TYPE { get; set; }
        /// <summary>
        /// 字符长度
        /// </summary>
        public string CHARACTER_MAXIMUM_LENGTH { get; set; }
        /// <summary>
        /// 是否Null
        /// </summary>
        public string IS_NULLABLE { get; set; }
        /// <summary>
        /// 列默认值
        /// </summary>
        public string COLUMN_DEFAULT { get; set; }
        /// <summary>
        /// 列备注
        /// </summary>
        public string COLUMN_COMMENT { get; set; }
    }
}
