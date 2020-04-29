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
        public string FileSavePath { get; set; }
        public string TextCode { get; set; }
        public string FinalCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract void SaveToFile();
        /// <summary>
        /// 
        /// </summary>
        public abstract void LoadByFile();
     

        public virtual void LoadByString(string content)
        {
            FinalCode = content;
        }

        public virtual string ToSaveString()
        {
            return this.FinalCode;
        }
    }
}
