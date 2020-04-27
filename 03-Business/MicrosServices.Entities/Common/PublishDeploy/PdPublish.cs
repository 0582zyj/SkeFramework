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
    /// 发布信息
    /// </summary>
    public class PdPublish : IEntity
    {
        public const string TableName = "pd_publish";
        [KeyAttribute(true)]
        [Description("ID")]
        public Int64 id { get; set; }
        [Description("项目编码")]
        public Int64 ProjectNo { get; set; }
        [Description("服务器编号")]
        public Int64 ServerNo { get; set; }
        [Description("发布命令")]
        public string PublishCmd { get; set; }
        [Description("发布配置文件")]
        public string PublishProfile { get; set; }
        [Description("项目输出目录")]
        public string WebProjectOutputDir { get; set; }
        [Description("输出路径")]
        public string OutputPath { get; set; }
        [Description("操作员")]
        public string InputUser { get; set; }
        [Description("操作时间")]
        public DateTime InputTime { get; set; }
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
