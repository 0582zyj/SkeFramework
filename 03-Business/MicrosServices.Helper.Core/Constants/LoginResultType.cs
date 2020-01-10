using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Core.Enums.ExtendAttribute;

namespace MicrosServices.Helper.Core.Constants
{
    /// <summary>
    /// 登录结果枚举
    /// </summary>
    public enum LoginResultType
    {
        [EnumAttribute(910, "账号或者密码为空")] ERROR_PARA,
        [EnumAttribute(911, "账号不存在")] ERROR_USER_NOT_EXIST,
        [EnumAttribute(912, "账号停用")] ERROR_USER_INACTIVE,
        [EnumAttribute(913, "密码输入错误次数超过三次")] ERROR_PASSWORD_TOO_MUCH,
        [EnumAttribute(914, "密码错误")] ERROR_PASSWORD,
        [EnumAttribute(200, "登录成功")] SUCCESS_LOGIN,


        [EnumAttribute(200, "注册成功")] SUCCESS_REGISTOR,
        [EnumAttribute(400, "注册失败")] FAILED_REGISTOR,

        [EnumAttribute(810, "邮箱不存在")] ERROR_EMAIL_NOT_EXIST,
        [EnumAttribute(811, "电话号码不存在")] ERROR_PHONE_NOT_EXIST,


        [EnumAttribute(200, "注销成功")] SUCCESS_CANCEL,
        [EnumAttribute(400, "注销失败")] FAILED_CANCEL,

    }
}
