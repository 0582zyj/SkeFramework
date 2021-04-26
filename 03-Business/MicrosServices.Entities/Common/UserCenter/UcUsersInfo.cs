using SkeFramework.DataBase.Common.AttributeExtend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Common.UserCenter
{
    public class UcUsersInfo : IEntity
    {
        public const string TableName = "uc_users_info";
        [KeyAttribute(true)]
        [Description("主键")]
        public int id { get; set; }
        [Description("用户账号")]
        public string UserNo { get; set; }
        [Description("真实名称")]
        public string FullName { get; set; }
        [Description("用户昵称")]
        public string NickName { get; set; }
        [Description("身份证号码")]
        public string IdentityCard { get; set; }
        [Description("居住地址")]
        public string Address { get; set; }
        [Description("工作地址")]
        public string WorkAddress { get; set; }
        [Description("性别【0,1,2来表示，未知,男,女】")]
        public int Gender { get; set; }
        [Description("生日")]
        public string Birthday { get; set; }
        [Description("QQ")]
        public string QQ { get; set; }
        [Description("WeChat")]
        public string WeChat { get; set; }
        [Description("个性签名")]
        public string Signature { get; set; }
        [Description("头像")]
        public string ImageHead { get; set; }
        [Description("备注")]
        public string Note { get; set; }
        [Description("创建人")]
        public string InputUser { get; set; }
        [Description("创建时间")]
        public DateTime InputTime { get; set; }
        [Description("更新人")]
        public string UpdateUser { get; set; }
        [Description("更新时间")]
        public DateTime UpdateTime { get; set; }
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

