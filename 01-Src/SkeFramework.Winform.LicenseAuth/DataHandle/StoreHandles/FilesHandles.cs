using Newtonsoft.Json.Linq;
using SkeFramework.Winform.SoftAuthorize.DataHandle.Securitys;
using SkeFramework.Winform.SoftAuthorize.DataHandle.StoreHandles.Abstract;
using SkeFramework.Winform.SoftAuthorize.DataUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.DataHandle.StoreHandles
{
    /// <summary>
    /// 文件存储功能的基类，包含了文件存储路径，存储方法等
    /// </summary>
    /// <remarks>
    public class FilesHandles : ProxySaveBase
    {

        #region 构造函数
        /// <summary>
        /// 实例化一个文件存储的基类
        /// </summary>
        public FilesHandles() : base(new DesHandle())
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
            if (FileSavePath != "")
            {
                if (File.Exists(FileSavePath))
                {
                    using (StreamReader sr = new StreamReader(FileSavePath, Encoding.Default))
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
            if (FileSavePath != "")
            {
                using (StreamWriter sw = new StreamWriter(FileSavePath, false, Encoding.Default))
                {
                    sw.Write(ToSaveString());
                    sw.Flush();
                }
            }
        }
        #endregion

    }
}
