using SkeFramework.DataBase.Common.AttributeExtend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Common.PublishDeploy
{
    /// <summary>
    /// 项目表信息
    /// </summary>
    public class PdProject : IEntity
    {
        public const string TableName = "pd_project";
        [KeyAttribute(true)]
        [Description("ID")]
        public Int64 id { get; set; }
        [Description("编码")]
        public Int64 ProjectNo { get; set; }
        [Description("名称")]
        public string Name { get; set; }
        [Description("版本控制类型[Git/SVN]")]
        public string VersionType { get; set; }
        [Description("版本管理地址")]
        public string VersionUrl { get; set; }
        [Description("源代码路径")]
        public string SourcePath { get; set; }
        [Description("打包程序路径")]
        public string MSBuildPath { get; set; }
        [Description("打包命令文件路径")]
        public string ProjectFile { get; set; }
        [Description("通知邮箱列表")]
        public string notifyEmails { get; set; }
        [Description("操作员")]
        public string InputUser { get; set; }
        [Description("操作时间")]
        public DateTime InputTime { get; set; }
        [Description("")]
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
