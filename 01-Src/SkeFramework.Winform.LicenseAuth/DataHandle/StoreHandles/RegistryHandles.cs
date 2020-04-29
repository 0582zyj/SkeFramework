using SkeFramework.Winform.LicenseAuth.DataHandle.Securitys;
using SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles.Abstract;
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
        public RegistryHandles():base(new DesHandle())
        {
            
        }
        /// <summary>
        /// 实例化注册表存储的基类
        /// </summary>
        public RegistryHandles(ISecurityHandle security) : base(security)
        {
        }
        #endregion

        public override void LoadByFile()
        {
            throw new NotImplementedException();
        }

        public override void SaveToFile()
        {
            throw new NotImplementedException();
        }
    }
}
