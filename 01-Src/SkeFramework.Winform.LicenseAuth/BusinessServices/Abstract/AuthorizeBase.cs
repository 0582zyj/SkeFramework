using Newtonsoft.Json;
using SkeFramework.Core.Common.Enums;
using SkeFramework.Core.NetLog;
using SkeFramework.Winform.LicenseAuth.DataEntities;
using SkeFramework.Winform.LicenseAuth.DataEntities.Constant;
using SkeFramework.Winform.LicenseAuth.DataHandle.Securitys;
using SkeFramework.Winform.LicenseAuth.DataHandle.StoreHandles;
using SkeFramework.Winform.LicenseAuth.DataUtils;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// 注册码保存处理
        /// </summary>
        private ISaveHandles licenseSaveHandles;
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
        public string LicensePath { get { return licenseSaveHandles.FileSavePath; }  set { licenseSaveHandles.FileSavePath = value; }  }
        #endregion

        public AuthorizeBase(ISaveHandles save, ISecurityHandle  security)
        {
            licenseSaveHandles = save;
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
                licenseSaveHandles.SaveToFile();
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
                licenseSaveHandles.LoadByFile();
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
            if (result.ValidateResponses() || result.code==(int)ErrorCodeEnums.TokenExpired)
            {
                licenseSaveHandles.FinalCode = code;
                SaveToFile();
            }
            else
            {
                if (File.Exists(this.LicensePath))
                {
                    File.Delete(this.LicensePath);
                    LogAgent.Info("CheckAuthorize:删除文件" + this.LicensePath + " " + result.msg);
                }
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
            if (licenseSaveHandles.FinalCode == null)
            {
                return new JsonResponse((int)ErrorCodeEnums.LicenseNotExist, "找不到本地激活码,请重新联网激活");
            }
            return VerifyCode(licenseSaveHandles.FinalCode);
        }
        /// <summary>
        /// 获取本机的机器码
        /// </summary>
        /// <returns>机器码字符串</returns>
        public string GetMachineCodeString()
        {
            return SystemUtil.Value();
        }
        /// <summary>
        /// 校验服务器时间
        /// </summary>
        /// <param name="TimeSpan"></param>
        /// <returns></returns>
        public JsonResponse CheckAuthorizeServerTime(long TimeSpan)
        {
            JsonResponse jsonResponse = JsonResponse.Failed.Clone() as JsonResponse;
            try
            {
                jsonResponse = CheckLocalAuthorize();
                if (jsonResponse.ValidateResponses())
                {
                     jsonResponse= CheckLocalServerTime(TimeSpan, jsonResponse);
                }
            }
            catch (Exception ex)
            {
                LogAgent.Info(ex.ToString());
            }
            return jsonResponse;
        }

        /// <summary>
        /// 检查时间
        /// </summary>
        /// <param name="TimeSpan"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public JsonResponse CheckLocalServerTime(long timeSpan, JsonResponse response)
        {
            if(!(response.data is Dictionary<string, object>))
            {
                LogAgent.Info("CheckLocalServerTime:"+JsonConvert.SerializeObject( response));
                return response;
            }
            Dictionary<string, object> Payload = response.data as Dictionary<string, object>;
            if (Payload.ContainsKey("exp"))
            {
                long exp = Convert.ToInt64(Payload["exp"]);
                if (timeSpan < exp)
                {
                    response.code = JsonResponse.SuccessCode;
                    response.data = Payload;
                    response.msg = "校准成功";
                    return response;
                }
                else
                {
                    response.code = (int)ErrorCodeEnums.TokenExpired;
                    response.msg = ErrorCodeEnums.TokenExpired.GetEnumDescription();
                    LogAgent.Info(String.Format("注册码已过期:localExp:{0},ServerExp:{1}", exp, timeSpan));
                    if (File.Exists(this.LicensePath))
                    {
                        File.Delete(this.LicensePath);
                        LogAgent.Info("CheckAuthorize:删除文件" + this.LicensePath + " " + response.msg);
                    }
                }
            }
            return response;
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
            JsonResponse jsonResponse = JsonResponse.Failed.Clone() as JsonResponse;
            if (code == null)
            {
                jsonResponse.msg = "激活码为空";
                return jsonResponse;
            }
            try
            {
                return securityHandle.Validate(code, GetMachineCodeString());
            }
            catch (Exception ex)
            {
                jsonResponse.msg = ex.ToString();
            }
            return jsonResponse;
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
