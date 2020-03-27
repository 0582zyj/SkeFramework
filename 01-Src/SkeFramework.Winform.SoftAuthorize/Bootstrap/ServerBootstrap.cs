using SkeFramework.Winform.SoftAuthorize.DataForm;
using SkeFramework.Winform.SoftAuthorize.DataHandle;
using SkeFramework.Winform.SoftAuthorize.DataHandle.SecurityHandles;
using SkeFramework.Winform.SoftAuthorize.DataHandle.Securitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkeFramework.Winform.SoftAuthorize.Bootstrap
{
    /// <summary>
    /// 启动程序
    /// </summary>
    public class ServerBootstrap
    {
        /// <summary>
        /// 安全策略
        /// </summary>
        private ISecurityHandle SecurityHandle;

        public ServerBootstrap()
        {
            SecurityHandle = new JwtHandle(); 
        }
        /// <summary>
        /// 可注入不同的加密策略
        /// </summary>
        /// <param name="security"></param>
        public ServerBootstrap(ISecurityHandle security)
        {
            SecurityHandle = security;
        }
        /// <summary>
        /// 初始化授权码校验
        /// </summary>
        /// <returns></returns>
        public virtual bool InitAuthorize()
        {
            AuthorizeAgent.Instance().FileSavePath = Application.StartupPath + @"\license.key"; // 设置存储激活码的文件，该存储是加密的
       
            // 检测激活码是否正确，没有文件，或激活码错误都算作激活失败
            if (!AuthorizeAgent.Instance().IsAuthorizeSuccess(AuthorizeEncrypted))
            {
                // 显示注册窗口
                using (FormAuthorize form =
                    new FormAuthorize(
                        "请根据机器码联系管理员获取注册码",
                        AuthorizeDncrypted))
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
        /// <summary>
        /// 一个自定义的加密方法，传入一个原始数据，返回一个加密结果
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public virtual string AuthorizeEncrypted(string origin)
        {
            // 此处使用了组件支持的DES对称加密技术
            return SecurityHandle.Encrypt(origin);
        }

        public virtual string AuthorizeDncrypted(string origin)
        {
            // 此处使用了组件支持的DES对称加密技术
            return SecurityHandle.Decrypt(origin);
        }
    }
}
