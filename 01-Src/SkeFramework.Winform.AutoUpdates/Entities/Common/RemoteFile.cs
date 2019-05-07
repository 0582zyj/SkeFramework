using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SkeFramework.Winform.AutoUpdates.Entities.Bases;

namespace SkeFramework.Winform.AutoUpdates.Entities.Common
{
    /// <summary>
    /// 服务器文件信息
    /// </summary>
    public class RemoteFile : BaseFile
    {
        #region 私有字段
        /// <summary>
        /// 服务器更新地址
        /// </summary>
        private string url = "";
        /// <summary>
        /// 是否需要重启
        /// </summary>
        private bool needRestart = false;
        #endregion

        #region 公共属性
        /// <summary>
        /// 文件路径
        /// </summary>
        public override string Path { get { return path; } }
        /// <summary>
        /// 上一版本号
        /// </summary>
        public override string LastVer { get { return lastver; } }
        /// <summary>
        /// 文件大小
        /// </summary>
        public override int Size { get { return size; } }
        /// <summary>
        /// 版本号
        /// </summary>
        public override string Version { get { return version; } }
        /// <summary>
        /// 是否需要重启
        /// </summary>
        public bool NeedRestart { get { return needRestart; } }
        /// <summary>
        /// 服务器更新地址
        /// </summary>
        public string Url { get { return url; } }
        #endregion

        #region 构造方法
        public RemoteFile(XmlNode node)
            : base(node.Attributes["path"].Value, node.Attributes["lastver"].Value,
            Convert.ToInt32(node.Attributes["size"].Value), node.Attributes["version"].Value)
        {
            this.url = node.Attributes["url"].Value;
            this.needRestart = Convert.ToBoolean(node.Attributes["needRestart"].Value);
        }
        #endregion
    }
}
