using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.BusinessServices
{
    /// <summary>
    /// 授权接口
    /// </summary>
    public interface IAuthorize
    {
        /// <summary>
        /// 保存路径
        /// </summary>
        string LicensePath { get; set; }
        /// <summary>
        /// 获取机器码
        /// </summary>
        /// <returns></returns>
        string GetMachineCodeString();
        /// <summary>
        /// 检查该注册码是否是正确的注册码
        /// </summary>
        /// <param name="code">注册码信息</param>
        /// <returns>是否注册成功</returns>
        bool CheckAuthorize(string code);
        /// <summary>
        /// 检测授权是否成功
        /// </summary>
        /// <returns>是否成功授权</returns>
        bool CheckLocalAuthorize();
    }
}
