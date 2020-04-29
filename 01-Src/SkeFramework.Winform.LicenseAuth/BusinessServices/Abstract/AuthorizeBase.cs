using SkeFramework.Winform.LicenseAuth.DataEntities;
using SkeFramework.Winform.LicenseAuth.DataHandle.Securitys;
using SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles;
using SkeFramework.Winform.LicenseAuth.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.LicenseAuth.BusinessServices.Abstract
{
    /// <summary>
    /// 授权基类
    /// </summary>
    public abstract class AuthorizeBase : IAuthorize,IDisposable
    {
        #region 私有成员
        /// <summary>
        /// 指示是否加载过文件信息
        /// </summary>
        private bool HasLoadByFile { get; set; } = false;
        /// <summary>
        /// 注册码保存处理
        /// </summary>
        private ISaveHandles saveHandles;
        /// <summary>
        /// 注册码加密处理
        /// </summary>
        private ISecurityHandle securityHandle;
        /// <summary>
        /// 文件存储的同步锁
        /// </summary>

        private ThreadHybirdLock HybirdLock;
        #endregion
        #region 共有成员
        /// <summary>
        /// 注册码保存地址
        /// </summary>
        public string LicensePath { get => saveHandles.FileSavePath; set => saveHandles.FileSavePath = value; }
        #endregion

        public AuthorizeBase(ISaveHandles save, ISecurityHandle  security)
        {
            saveHandles = save;
            securityHandle = security;
            HybirdLock = new ThreadHybirdLock();
        }

        #region 授权码操作
        /// <summary>
        /// 使用特殊加密算法加密数据
        /// </summary>
        protected void SaveToFile()
        {
            HybirdLock.Enter();
            try
            {
                saveHandles.SaveToFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                HybirdLock.Leave();
            }
        }
        /// <summary>
        /// 使用特殊解密算法解密数据
        /// </summary>
        protected void LoadByFile()
        {
            HybirdLock.Enter();
            try
            {
                saveHandles.LoadByFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                HybirdLock.Leave();
            }
        }
        #endregion

        #region 注册授权
        /// <summary>
        /// 检查注册码是否正确
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public JsonResponse CheckAuthorize(string code)
        {
            JsonResponse result = VerifyCode(code);
            if (result.ValidateResponses())
            {
                saveHandles.FinalCode = code;
                SaveToFile();
            }
            return result;
        }
        /// <summary>
        /// 检查本地注册码
        /// </summary>
        /// <returns></returns>
        public JsonResponse CheckLocalAuthorize()
        {
            LoadByFile();
            return VerifyCode(saveHandles.FinalCode);
        }
        /// <summary>
        /// 获取本机的机器码
        /// </summary>
        /// <returns>机器码字符串</returns>
        public string GetMachineCodeString()
        {
            return SystemUtil.Value();
        }
        #endregion

        #region 可重写
        /// <summary>
        /// 校验注册码方式
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected virtual JsonResponse VerifyCode(string code)
        {
            if (code == null)
            {
                return JsonResponse.Failed;
            }
            try
            {
                return securityHandle.Validate(code, GetMachineCodeString());
            }
            catch (Exception)
            {
               return JsonResponse.Failed;
            }
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
