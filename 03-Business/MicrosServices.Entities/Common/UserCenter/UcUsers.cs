using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using SkeFramework.DataBase.Common.AttributeExtend;

namespace MicrosServices.Entities.Common
{
    public class UcUsers : IEntity
    {
        public const string TableName = "uc_users";
        [KeyAttribute(true)]
        [Description("主键")]
        public int id { get; set; }
        [Description("用户账号")]
        public string UserNo { get; set; }
        [Description("用户名")]
        public string UserName { get; set; }
        [Description("登录密码")]
        public string Password { get; set; }
        [Description("是否过期")]
        public int IsExpire { get; set; }
        [Description("电话")]
        public string Phone { get; set; }
        [Description("邮箱")]
        public string Email { get; set; }
        [Description("创建人")]
        public string InputUser { get; set; }
        [Description("创建时间")]
        public DateTime InputTime { get; set; }
        [Description("更新人")]
        public string UpdateUser { get; set; }
        [Description("更新时间")]
        public DateTime UpdateTime { get; set; }
        [Description("是否启用")]
        public int Enabled { get; set; }
        [Description("上次登录IP")]
        public string LastLoginIP { get; set; }
        [Description("上次登录时间")]
        public DateTime LastLoginTime { get; set; }
        [Description("上次登录MAC地址")]
        public string LastLoginMac { get; set; }
        [Description("错误登录次数")]
        public int FailedLoginCount { get; set; }
        [Description("错误登录时间")]
        public DateTime FailedLoginTime { get; set; }

        public string GetTableName()
        {
            return TableName;
        }
        public string GetKey()
        {
            return "id";
        }
    }
}
