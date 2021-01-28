using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.LicenseAuth.DataEntities.Constant
{
    /// <summary>
    /// 静态数据
    /// </summary>
    public class LicenseConstData
    {
        /// <summary>
        /// Token路径
        /// </summary>
        public const string LicensePath = @"\license.key";
        /// <summary>
        /// 密钥路径
        /// </summary>
        public const string KeyPath = @"\sk.key";
        /// <summary>
        /// 注册文件路径【密钥】
        /// </summary>
        public const string KeyRegistyPath = "SOFTWARE\\UTWL\\SmartHomeIDE";
        /// <summary>
        /// 电脑唯一码【注册表Key】
        /// </summary>
        public const string MacCodeKey = "MacCodeKey";
        /// <summary>
        /// 私钥【注册表Key】
        /// </summary>
        public const string SecurityKey = "SecurityKey";
        /// <summary>
        /// 激活码【注册表Key】
        /// </summary>
        public const string FinalCodeKey = "FinalCodeKey";
        /// <summary>
        /// Token路径【注册表Key】
        /// </summary>
        public const string LicenseKey = "LicenseKey";

    }
}
