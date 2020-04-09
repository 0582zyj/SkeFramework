using System;
using SkeFramework.Winform.SoftAuthorize.BusinessServices;
using System.Windows.Forms;

namespace SkeFramework.Winform.SoftAuthorize.DataHandle
{
    /// <summary>
    /// 授权代理
    /// </summary>
    public class AuthorizeAgent : IDisposable
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


        private IAuthorize authorize;

        #region 构造函数
        /// <summary>
        /// 实例化一个软件授权类
        /// </summary>
        public AuthorizeAgent()
        {
    
        }
        #endregion

        #region 授权校验
        /// <summary>
        /// 初始化授权码校验
        /// </summary>
        /// <returns></returns>
        public virtual bool InitAuthorize()
        {
            ////设置存储激活码的文件，该存储是加密的
            //AuthorizeAgent.Instance().FileSavePath = Application.StartupPath + @"\license.key";
            //// 检测激活码是否正确，没有文件，或激活码错误都算作激活失败
            //if (!AuthorizeAgent.Instance().IsAuthorizeSuccess(AuthorizeEncrypted))
            //{
            //    // 显示注册窗口
            //    using (FormAuthorize form =
            //        new FormAuthorize(
            //            "请根据机器码联系管理员获取注册码",
            //            AuthorizeDncrypted))
            //    {
            //        if (form.ShowDialog() != DialogResult.OK)
            //        {
            //            // 授权失败，退出
            //            return false;
            //        }
            //    }
            //}
            return true;
        }
        #endregion

        #region 释放资源
        /// <summary>
        /// 释放标记
        /// </summary>
        private bool disposed;
        /// <summary>执行与释放或重置非托管资源关联的应用程序定义的任务。</summary>
        public void Dispose()
        {
            //必须为true
            Dispose(true);
            //通知垃圾回收器不再调用终结器
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 非必需的，只是为了更符合其他语言的规范，如C++、java
        /// </summary>
        public void Close()
        {
            Dispose();
        }
        /// <summary>
        /// 非密封类可重写的Dispose方法，方便子类继承时可重写
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (HybirdLock != null)
            {
                HybirdLock.Dispose();
            }
            //告诉自己已经被释放
            disposed = true;
        }
        #endregion
    }
}
