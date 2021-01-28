
using SkeFramework.Winform.LicenseAuth.DataEntities.Constant;
using SkeFramework.Winform.LicenseAuth.DataHandle.Securitys;
using SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles.Abstract;
using SkeFramework.Winform.LicenseAuth.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles
{
    /// <summary>
    /// 注册表保存
    /// </summary>
    public class RegistryHandles : ProxySaveBase
    {
        #region 构造函数
        /// <summary>
        /// 实例化注册表存储的基类
        /// </summary>
        public RegistryHandles() : base(new DesHandle())
        {

        }
        /// <summary>
        /// 实例化注册表存储的基类
        /// </summary>
        public RegistryHandles(ISecurityHandle security) : base(security)
        {
        }
        #endregion


        #region 保存加载文件

        /// <summary>
        /// 从文件读取数据
        /// </summary>
        public override void LoadByFile()
        {
            string newValue = RegistryUtil.GetRegeditkeyValue(this.FileSavePath,this.FileName);
            LoadByString(newValue);
        }
        /// <summary>
        /// 保存数据到文件
        /// </summary>
        public override void SaveToFile()
        {
            string value = ToSaveString();
            RegistryUtil.SetSecurityLey(value, this.FileSavePath, this.FileName);
        }
        #endregion
    }
}
