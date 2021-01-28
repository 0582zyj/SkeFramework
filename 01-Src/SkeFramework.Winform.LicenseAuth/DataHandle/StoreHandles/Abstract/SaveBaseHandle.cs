using SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles.Abstract
{
    /// <summary>
    /// 密钥保存基类
    /// </summary>
    public abstract class SaveBaseHandle : ISaveHandles
    {
        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string FileSavePath { get; set; }
        /// <summary>
        /// 文件存储的名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 内容关键词
        /// </summary>
        public string TextCode { get; set; }
        /// <summary>
        /// 实际内容
        /// </summary>
        public string FinalCode { get; set; }
        /// <summary>
        /// 保存
        /// </summary>
        public abstract void SaveToFile();
        /// <summary>
        /// 读取
        /// </summary>
        public abstract void LoadByFile();
        /// <summary>
        /// 处理数据后返回原始数据
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadByString(string content)
        {
            FinalCode = content;
        }
        /// <summary>
        /// 是否对内容进行特殊处理
        /// </summary>
        /// <returns></returns>
        public virtual string ToSaveString()
        {
            return this.FinalCode;
        }
    }
}
