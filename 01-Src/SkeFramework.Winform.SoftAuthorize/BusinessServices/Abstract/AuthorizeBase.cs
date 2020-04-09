using SkeFramework.Winform.SoftAuthorize.DataHandle.Securitys;
using SkeFramework.Winform.SoftAuthorize.DataHandle.StoreHandles;
using SkeFramework.Winform.SoftAuthorize.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.BusinessServices.Abstract
{
    /// <summary>
    /// 授权基类
    /// </summary>
    public abstract class AuthorizeBase : IAuthorize
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
        public string FileSavePath { get { return saveHandles.FileSavePath; } set { saveHandles.FileSavePath = value; } }
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
        public bool CheckAuthorize(string code)
        {
            bool result = VerifyCode(code);
            if (result)
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
        public bool CheckLocalAuthorize()
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
        protected virtual bool VerifyCode(string code)
        {
            return GetMachineCodeString() == securityHandle.Decrypt(saveHandles.FinalCode);
        }
        #endregion
    }
}
