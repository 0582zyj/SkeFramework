﻿using SkeFramework.Winform.LicenseAuth.BusinessServices;
using SkeFramework.Winform.LicenseAuth.BusinessServices.Abstract;
using SkeFramework.Winform.LicenseAuth.DataEntities.Constant;
using SkeFramework.Winform.LicenseAuth.DataForm;
using SkeFramework.Winform.LicenseAuth.DataHandle;
using SkeFramework.Winform.LicenseAuth.DataHandle.SecurityHandles;
using SkeFramework.Winform.LicenseAuth.DataHandle.Securitys;
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

        public ServerBootstrap()
        {
        }


        public ServerBootstrap SetSecurityType(SecurityTypeEnums securityType)
        {
            if ((int)securityType < 1 || (int)securityType > 2) throw new ArgumentException("SecurityType must be set");
            SecurityType = securityType;
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
            switch (SecurityType)
            {
                case SecurityTypeEnums.DES:
                    this.SecurityHandle=new DesHandle();
                    break;
                case SecurityTypeEnums.JWT:
                    this.SecurityHandle = new JwtHandle();
                    break;
                default:
                    throw new InvalidOperationException("加密类型暂未实现");
            }
            return new DefaultAuthorizeProxy(this.SecurityHandle);
        }
    }
}
