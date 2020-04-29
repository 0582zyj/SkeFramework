using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles
{
    /// <summary>
    /// 支持字符串信息加载存储的接口，定义了几个通用的方法
    /// </summary>
    public interface ISaveHandles
    {       
        /// <summary>
        /// 不使用解密方法从文件读取数据
        /// </summary>
        void LoadByFile();
        /// <summary>
        /// 不使用加密方法保存数据到文件
        /// </summary>
        void SaveToFile();
        /// <summary>
        /// 文件路径的存储
        /// </summary>
        string FileSavePath { get; set; }
    
        /// <summary>
        /// 注册码
        /// </summary>
        string FinalCode { get; set; }
    }
}
