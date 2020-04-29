using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Common
{
    /// <summary>
    /// 树节点信息
    /// </summary>
    public class TreeNodeInfo
    {
        /// <summary>
        /// 节点ID 
        /// </summary>
        public string TreeNo { get; set; }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public string ParentNo { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 展开图标
        /// </summary>
        public string IconOpen { get; set; }
        /// <summary>
        /// 折叠图标
        /// </summary>
        public string IconClose { get; set; }
        /// <summary>
        /// 展开
        /// </summary>
        public bool Open { get; set; } = false;
        /// <summary>
        /// 没有子节点
        /// </summary>
        public bool IsParent { get; set; } = false;
        

    }
}
