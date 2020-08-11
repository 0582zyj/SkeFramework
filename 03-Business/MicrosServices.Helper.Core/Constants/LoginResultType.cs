using SkeFramework.Core.Common.ExtendAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Constants
{
    /// <summary>
    /// 登录结果枚举
    /// </summary>
    public enum LoginResultType
    {
        [EnumAttribute("账号或者密码为空")] ERROR_PARA = 910,
        [EnumAttribute("账号不存在")] ERROR_USER_NOT_EXIST = 911,
        [EnumAttribute("账号停用")] ERROR_USER_INACTIVE = 912,
        [EnumAttribute("密码输入错误次数超过三次")] ERROR_PASSWORD_TOO_MUCH = 913,
        [EnumAttribute("密码错误")] ERROR_PASSWORD = 914,
        [EnumAttribute("成功")] SUCCESS = 200,
        [EnumAttribute("失败")] FAILED = 400,
        [EnumAttribute("登录成功")] SUCCESS_LOGIN = 201,


        [EnumAttribute("注册成功")] SUCCESS_REGISTOR = 202,
        [EnumAttribute("注册失败")] FAILED_REGISTOR = 401,

        [EnumAttribute("邮箱不存在")] ERROR_EMAIL_NOT_EXIST = 810,
        [EnumAttribute("电话号码不存在")] ERROR_PHONE_NOT_EXIST = 811,


        [EnumAttribute("注销成功")] SUCCESS_CANCEL = 203,
        [EnumAttribute("注销失败")] FAILED_CANCEL = 402,


        [EnumAttribute("代码拉取成功")] SUCCESS_PUBLISHGIT = 204,
        [EnumAttribute("发布成功")] SUCCESS_PUBLISHCMD = 205,

    }
}
