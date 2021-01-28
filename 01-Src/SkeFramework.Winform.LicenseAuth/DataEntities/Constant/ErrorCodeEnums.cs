using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.LicenseAuth.DataEntities.Constant
{
    /// <summary>
    /// 错误码定义
    /// </summary>
    public enum ErrorCodeEnums:int
    {
        [Description("首次激活")]
        InActive = 10000,
        [Description("私钥认证失效,请重新联网激活！")]
        SignatureError = 11100,
        [Description("该注册码不在有效期内！")]
        TokenExpired = 11102,
        [Description("激活码为空")]
        LicenseNotExist = 11103,
        [Description("密钥文件不存在，请重新联网激活！")]
        SignatureKeyEmpty = 11104,
        [Description("电脑机器码已变化,请重新联网激活！")]
        MacCodeKeyChange = 11105,
    }
}
