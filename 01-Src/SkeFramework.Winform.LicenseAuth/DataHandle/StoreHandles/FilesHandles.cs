using Newtonsoft.Json.Linq;
using SkeFramework.Winform.LicenseAuth.DataHandle.Securitys;
using SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles.Abstract;
using SkeFramework.Winform.LicenseAuth.DataUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles
{
    /// <summary>
    /// 文件存储功能的基类，包含了文件存储路径，存储方法等
    /// </summary>
    /// <remarks>
    public class FilesHandles : ProxySaveBase
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        private string fullFileName { get { return this.FileSavePath + this.FileName; } }

        #region 构造函数
        /// <summary>
        /// 实例化一个文件存储的基类
        /// </summary>
        public FilesHandles() : this(new DesHandle())
        {

        }
        /// <summary>
        /// 实例化一个文件存储的基类
        /// </summary>
        public FilesHandles(ISecurityHandle security) :base(security)
        {
        }
        #endregion

        #region 保存加载文件

        /// <summary>
        /// 从文件读取数据
        /// </summary>
        public override void LoadByFile()
        {
            if (!String.IsNullOrEmpty(fullFileName))
            {
                if (File.Exists(fullFileName))
                {
                    using (StreamReader sr = new StreamReader(fullFileName, Encoding.Default))
                    {
                        LoadByString(sr.ReadToEnd());
                    }
                }
            }
        }
        /// <summary>
        /// 保存数据到文件
        /// </summary>
        public override void SaveToFile()
        {
            if (!String.IsNullOrEmpty(fullFileName))
            {
                string msg = ToSaveString();
                if (String.IsNullOrEmpty(msg))
                    return;
                using (StreamWriter sw = new StreamWriter(fullFileName, false, Encoding.Default))
                {
                    sw.Write(msg);
                    sw.Flush();
                }
            }
        }
        #endregion

    }
}
