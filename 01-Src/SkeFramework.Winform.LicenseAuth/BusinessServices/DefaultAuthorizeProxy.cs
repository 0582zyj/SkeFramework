using SkeFramework.Winform.LicenseAuth.BusinessServices.Abstract;
using SkeFramework.Winform.LicenseAuth.DataHandle.Securitys;
using SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkeFramework.Winform.LicenseAuth.BusinessServices
{
    /// <summary>
    /// 默认授权实现【采取文件保存方式】
    /// </summary>
    public class DefaultAuthorizeProxy: AuthorizeBase
    {

        public DefaultAuthorizeProxy(ISecurityHandle security):this(new FilesHandles(),security)
        {
          
        }

        public DefaultAuthorizeProxy(ISaveHandles save, ISecurityHandle security):base(save,security)
        {
            this.LicensePath = Application.StartupPath + @"\license.key";
        }
    }
}
