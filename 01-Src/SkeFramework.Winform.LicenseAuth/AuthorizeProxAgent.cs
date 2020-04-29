using System;
using SkeFramework.Winform.LicenseAuth.BusinessServices;
using System.Windows.Forms;
using SkeFramework.Winform.LicenseAuth.Bootstrap;
using SkeFramework.Winform.LicenseAuth.DataEntities.Constant;
using SkeFramework.Winform.LicenseAuth.DataForm;
using SkeFramework.Winform.LicenseAuth.DataEntities;

namespace SkeFramework.Winform.LicenseAuth.DataHandle
{
    /// <summary>
    /// 授权代理
    /// </summary>
    public class AuthorizeAgent
    {
        #region 单例模式
        /// <summary>
        /// 协议管理器
        /// </summary>
        private static AuthorizeAgent mSingleInstance;
        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static AuthorizeAgent Instance()
        {
            if (null == mSingleInstance)
            {
                mSingleInstance = new AuthorizeAgent();
            }
            return mSingleInstance;
        }
        #endregion

        private IAuthorize authorize = null;
        #region 构造函数
        /// <summary>
        /// 实例化一个软件授权类
        /// </summary>
        public AuthorizeAgent()
        {
            authorize = new ServerBootstrap().SetSecurityType(SecurityTypeEnums.JWT).Build();
        }
        #endregion

        #region 授权校验
        /// <summary>
        /// 初始化授权码校验
        /// </summary>
        /// <returns></returns>
        public virtual bool InitAuthorize()
        {
            // 检测激活码是否正确，没有文件，或激活码错误都算作激活失败
            JsonResponse response = authorize.CheckLocalAuthorize();
            if (!response.ValidateResponses())
            {
                // 显示注册窗口
                using (FormAuthorize form =
                    new FormAuthorize(
                        "请根据机器码联系管理员获取注册码", authorize))
                {
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        // 授权失败，退出
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion


    }
}
