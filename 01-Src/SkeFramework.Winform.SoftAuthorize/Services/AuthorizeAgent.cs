using Newtonsoft.Json.Linq;
using SkeFramework.Winform.SoftAuthorize.DataUtils;
using SkeFramework.Winform.SoftAuthorize.Services.Files;
using SkeFramework.Winform.SoftAuthorize.Services.Securitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.Services
{
    /// <summary>
    /// 授权代理
    /// </summary>
    public class AuthorizeAgent
    {
        /// <summary>
        /// 注册码描述文本
        /// </summary>
        public static readonly string TextCode = "Code";

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
        /// 机器码
        /// </summary>
        private string machine_code = "";
        /// <summary>
        /// 加密处理
        /// </summary>
        private ISecurityHandle securityHandle;
        /// <summary>
        /// 文件处理
        /// </summary>
        private ISoftFileSaveBase softFileSaveBase;
        #endregion

        #region 共有成员
        /// <summary>
        /// 指示系统是否处于试用运行
        /// </summary>
        public bool IsSoftTrial { get; set; } = false;
        /// <summary>
        /// 注册码保存地址
        /// </summary>
        public string FileSavePath { get { return softFileSaveBase.FileSavePath; } set { softFileSaveBase.FileSavePath = value; } }
        #endregion

        #region 构造函数
        /// <summary>
        /// 实例化一个软件授权类
        /// </summary>
        /// <param name="UseAdmin">是否使用管理员模式</param>
        public AuthorizeAgent(bool UseAdmin = false)
        {
            machine_code = SafeNativeMethods.GetInfo(UseAdmin);
            securityHandle = new MD5SecurityHandle();
            softFileSaveBase = new SoftFileSaveBase();
            softFileSaveBase.TextCode = TextCode;
        }

        #endregion

        #region Public Method
        /// <summary>
        /// 获取本机的机器码
        /// </summary>
        /// <returns>机器码字符串</returns>
        public string GetMachineCodeString()
        {
            return machine_code;
        }
        /// <summary>
        /// 获取需要保存的数据内容
        /// </summary>
        /// <returns>实际保存的内容</returns>
        public string ToSaveString()
        {
            return softFileSaveBase.ToSaveString();
        }
        /// <summary>
        /// 从字符串加载数据
        /// </summary>
        /// <param name="content">文件存储的数据</param>
        public void LoadByString(string content)
        {
            softFileSaveBase.LoadByString(content);
            HasLoadByFile = true;
        }
        /// <summary>
        /// 使用特殊加密算法加密数据
        /// </summary>
        public void SaveToFile() => softFileSaveBase.SaveToFile(m => securityHandle.Encrypt(m));
        /// <summary>
        /// 使用特殊解密算法解密数据
        /// </summary>
        public void LoadByFile() => softFileSaveBase.LoadByFile(m => securityHandle.Decrypt(m));
        /// <summary>
        /// 检查该注册码是否是正确的注册码
        /// </summary>
        /// <param name="code">注册码信息</param>
        /// <param name="encrypt">数据加密的方法，必须用户指定</param>
        /// <returns>是否注册成功</returns>
        public bool CheckAuthorize(string code, Func<string, string> encrypt)
        {
            if (code != encrypt(GetMachineCodeString()))
            {
                return false;
            }
            else
            {
                softFileSaveBase.FinalCode = code;
                SaveToFile();
                return true;
            }
        }

        /// <summary>
        /// 检测授权是否成功
        /// </summary>
        /// <param name="encrypt">数据加密的方法，必须用户指定</param>
        /// <returns>是否成功授权</returns>
        public bool IsAuthorizeSuccess(Func<string, string> encrypt)
        {
            if (IsReleaseVersion) return true;
            if (encrypt(GetMachineCodeString()) == softFileSaveBase.FinalCode)
            {
                return true;
            }
            else
            {
                softFileSaveBase.FinalCode = "";
                SaveToFile();
                return false;
            }
        }
        #endregion

    }
}
