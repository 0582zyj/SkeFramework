using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Winform.AutoUpdates.Entities.Bases;

namespace SkeFramework.Winform.AutoUpdates.Entities.Common
{
    /// <summary>
    /// 下载文件信息
    /// </summary>
    public class DownloadFileInfo : BaseFile
    {
        #region 私有字段
        /// <summary>
        /// 下载地址
        /// </summary>
        string downloadUrl = string.Empty;
        /// <summary>
        /// 文件名
        /// </summary>
        string fileName = string.Empty;
        #endregion

        #region 公共属性
        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadUrl { get { return downloadUrl; } }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileFullName { get { return fileName; } }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get { return System.IO.Path.GetFileName(FileFullName); } }
        /// <summary>
        /// 文件大小
        /// </summary>
        public override int Size { get { return size; } }
        #endregion

        #region 构造函数
        public DownloadFileInfo(string url, string name, string ver, int size, string versionid)
        {
            this.downloadUrl = url;
            this.fileName = name;
            this.lastver = ver;
            this.size = size;
            this.version = versionid;
        }
        #endregion
    }
}
