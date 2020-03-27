using Newtonsoft.Json.Linq;
using SkeFramework.Winform.SoftAuthorize.DataUtils;
using SkeFramework.Winform.SoftAuthorize.DataHandle.StoreHandles;
using SkeFramework.Winform.SoftAuthorize.DataHandle.Securitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #region 私有成员
        /// <summary>
        /// 是否正式发行版，是的话就取消授权
        /// </summary>
        public bool IsReleaseVersion { get; set; } = false;
        /// <summary>
        /// 指示是否加载过文件信息
        /// </summary>
        private bool HasLoadByFile { get; set; } = false;
        /// <summary>
        /// 文件处理
        /// </summary>
        private ISaveHandles softFileSaveBase;
        /// <summary>
        /// 文件存储的同步锁
        /// </summary>

        private ThreadHybirdLock HybirdLock;
        #endregion

        #region 共有成员
        /// <summary>
        /// 注册码保存地址
        /// </summary>
        public string FileSavePath { get { return softFileSaveBase.FileSavePath; } set { softFileSaveBase.FileSavePath = value; } }
        #endregion

        #region 构造函数
        /// <summary>
        /// 实例化一个软件授权类
        /// </summary>
        public AuthorizeAgent(bool isSoftTrial = false)
        {
            HybirdLock = new ThreadHybirdLock();
            softFileSaveBase = new FilesHandles();        
        }

        #endregion

        #region 授权码操作
        /// <summary>
        /// 获取本机的机器码
        /// </summary>
        /// <returns>机器码字符串</returns>
        public string GetMachineCodeString()
        {
            return SystemUtil.Value();
        }       
        /// <summary>
        /// 使用特殊加密算法加密数据
        /// </summary>
        public void SaveToFile()
        {
            HybirdLock.Enter();
            try
            {
                softFileSaveBase.SaveToFile();
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
        public void LoadByFile()
        {
            HybirdLock.Enter();
            try
            {
                softFileSaveBase.LoadByFile();
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
      
        #region 授权校验
        /// <summary>
        /// 检查该注册码是否是正确的注册码
        /// </summary>
        /// <param name="code">注册码信息</param>
        /// <param name="encrypt">数据加密的方法，必须用户指定</param>
        /// <returns>是否注册成功</returns>
        public bool CheckAuthorize(string code, Func<string, string> encrypt)
        {
            if (GetMachineCodeString() != encrypt(code))
            {
                return false;
            }
            softFileSaveBase.FinalCode = code;
            SaveToFile();
            return true;
        }

        /// <summary>
        /// 检测授权是否成功
        /// </summary>
        /// <param name="encrypt">数据加密的方法，必须用户指定</param>
        /// <returns>是否成功授权</returns>
        public bool IsAuthorizeSuccess(Func<string, string> encrypt)
        {
            if (IsReleaseVersion) return true;
            AuthorizeAgent.Instance().LoadByFile();
            if (encrypt(GetMachineCodeString()) == softFileSaveBase.FinalCode)
            {
                return true;
            }
            return false;
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
