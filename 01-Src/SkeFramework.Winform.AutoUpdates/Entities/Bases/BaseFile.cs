using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SkeFramework.Winform.AutoUpdates.Entities.Bases
{
    /// <summary>
    /// 基本类
    /// </summary>
    public class BaseFile
    {
        #region 私有字段
        /// <summary>
        /// 文件路径
        /// </summary>
        protected string path = "";
        /// <summary>
        /// 上一版本号
        /// </summary>
        protected string lastver = "";
        /// <summary>
        /// 文件大小
        /// </summary>
        protected int size = 0;
        /// <summary>
        /// 版本号
        /// </summary>
        protected string version = "";
        #endregion

        #region 公共属性
        /// <summary>
        /// 文件路径
        /// </summary>
        [XmlAttribute("path")]
        public virtual string Path { get { return path; } set { path = value; } }
        /// <summary>
        /// 上一版本号
        /// </summary>
        [XmlAttribute("lastver")]
        public virtual string LastVer { get { return lastver; } set { lastver = value; } }
        /// <summary>
        /// 文件大小
        /// </summary>
        [XmlAttribute("size")]
        public virtual int Size { get { return size; } set { size = value; } }
        /// <summary>
        /// 版本号
        /// </summary>
        [XmlAttribute("version")]
        public virtual string Version { get { return version; } set { version = value; } }
        #endregion

        #region 构造方法
        public BaseFile(string path, string ver, int size, string versionid)
        {
            this.path = path;
            this.lastver = ver;
            this.size = size;
            this.version = versionid;
        }

        public BaseFile()
        {
        }
        #endregion
    }
}
