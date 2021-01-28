using SkeFramework.Winform.LicenseAuth.BusinessServices;
using SkeFramework.Winform.LicenseAuth.BusinessServices.Abstract;
using SkeFramework.Winform.LicenseAuth.DataEntities.Constant;
using SkeFramework.Winform.LicenseAuth.DataForm;
using SkeFramework.Winform.LicenseAuth.DataHandle;
using SkeFramework.Winform.LicenseAuth.DataHandle.SecurityHandles;
using SkeFramework.Winform.LicenseAuth.DataHandle.Securitys;
using SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkeFramework.Winform.LicenseAuth.Bootstrap
{
    /// <summary>
    /// 启动程序
    /// </summary>
    public class ServerBootstrap: AbstractBootstrap
    {
        /// <summary>
        /// 加密方式
        /// </summary>
        private SecurityTypeEnums SecurityType;
        /// <summary>
        /// 密钥保存方式
        /// </summary>
        private SaveTypeEnums saveTypeEnums;
        /// <summary>
        /// 保存地址
        /// </summary>
        private string savePath;
        /// <summary>
        /// 注册表Key
        /// </summary>
        private string savePathName;



        public ServerBootstrap()
        {
            saveTypeEnums = SaveTypeEnums.FILE;
        }
        /// <summary>
        /// 设置安全策略类型
        /// </summary>
        /// <param name="securityType"></param>
        /// <returns></returns>
        public ServerBootstrap SetSecurityType(SecurityTypeEnums securityType)
        {
            if ((int)securityType < 1 || (int)securityType > 2) throw new ArgumentException("SecurityType must be set");
            SecurityType = securityType;
            return this;
        }
        /// <summary>
        /// 设置保存方式
        /// </summary>
        /// <param name="saveType"></param>
        /// <returns></returns>
        public ServerBootstrap SetSaveType(SaveTypeEnums saveType)
        {
            if ((int)saveType < 1 || (int)saveType > 2) throw new ArgumentException("SaveType must be set");
            saveTypeEnums = saveType;
            return this;
        }
        /// <summary>
        /// 设置保存地址
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ServerBootstrap SetSavePath(string path)
        {
            if (String.IsNullOrEmpty(path)) throw new ArgumentException("savePath must be set");
            savePath = path;
            return this;
        }

        /// <summary>
        /// 设置保存名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ServerBootstrap SetFileName(string key)
        {
            if (String.IsNullOrEmpty(key)) throw new ArgumentException("registeyKey must be set");
            savePathName = key;
            return this;
        }

        /// <summary>
        /// 校验参数
        /// </summary>
        public override void Validate()
        {
            if ( (int)SecurityType< 1 || (int)SecurityType > 2) throw new ArgumentException("SecurityType must be set");
        }
        /// <summary>
        /// 生成授权代理
        /// </summary>
        /// <returns></returns>
        protected override IAuthorize BuildInternal()
        {
            ISaveHandles licenseHandle = this.BuildSaveHandles();
            switch (SecurityType)
            {
                case SecurityTypeEnums.DES:
                    this.SecurityHandle=new DesHandle();
                    break;
                case SecurityTypeEnums.JWT:
                    this.SecurityHandle = new JwtHandle(BuildSkSaveHandles());
                    break;
                default:
                    throw new InvalidOperationException("加密类型暂未实现");
            }
            return new DefaultAuthorizeProxy(licenseHandle, this.SecurityHandle);
        }

        /// <summary>
        /// 生成授权代理
        /// </summary>
        /// <returns></returns>
        public override ISaveHandles BuildSaveHandles()
        {
            ISaveHandles saveHandles = null;
            switch (saveTypeEnums)
            {
                case SaveTypeEnums.FILE:
                    saveHandles = new FilesHandles();
                    saveHandles.FileSavePath = this.savePath;// Application.StartupPath + LicenseConstData.KeyPath;
                    saveHandles.FileName = this.savePathName;
                    break;
                case SaveTypeEnums.REGISTRY:
                    saveHandles = new RegistryHandles();
                    saveHandles.FileSavePath = this.savePath;// LicenseConstData.KeyRegistyPath;
                    saveHandles.FileName = this.savePathName;
                    break;
                default:
                    throw new InvalidOperationException("保存类型暂未实现");
            }
            return saveHandles;
        }

        /// <summary>
        /// 生成密钥处理类
        /// </summary>
        /// <returns></returns>
        public ISaveHandles BuildSkSaveHandles()
        {
            return this.SetSaveType(SaveTypeEnums.REGISTRY)
               .SetSavePath(LicenseConstData.KeyRegistyPath)
               .SetFileName(LicenseConstData.SecurityKey)
               .BuildSaveHandles(); 
        }
    }
}
