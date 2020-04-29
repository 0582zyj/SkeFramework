using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Common.ZTree
{
    /// <summary>
    /// 树节点数据源
    /// </summary>
   public class zTreeNodes
    {
        /// <summary>
        /// 展开
        /// </summary>
        public bool Open { get; set; }
        /// <summary>
        /// 没有子节点
        /// </summary>
        public bool IsParent { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }

        private List<zTreeNodes> _children;
        /// <summary>
        /// 子节点集合 
        /// </summary>
        public List<zTreeNodes> children
        {
            get
            {
                if (_children == null)
                {
                    return _children = new List<zTreeNodes>();
                }
                return _children;
            }
            set
            {
                _children = value;
            }
        }
    }
}
