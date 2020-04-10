using SkeFramework.Winform.SoftAuthorize.BusinessServices;
using SkeFramework.Winform.SoftAuthorize.DataHandle.SecurityHandles;
using SkeFramework.Winform.SoftAuthorize.DataHandle.Securitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.Bootstrap
{
    /// <summary>
    /// 引导程序抽象实现
    /// </summary>
    public abstract class AbstractBootstrap
    {
        /// <summary>
        /// 安全策略
        /// </summary>
        protected ISecurityHandle SecurityHandle;

        protected AbstractBootstrap()
        {
        }

        protected AbstractBootstrap(AbstractBootstrap other)
            : this()
        {

        }

        /// <summary>
        /// 创建模板
        /// </summary>
        /// <returns></returns>
        public IAuthorize Build()
        {
            Validate();
            //BuildStoreHandles();
            return BuildInternal();
        }
        /// <summary>
        /// 校验参数
        /// </summary>
        public abstract void Validate();
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        protected abstract IAuthorize BuildInternal();
    }
}
