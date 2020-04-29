using CodeBuilder.BLL.Achieve;
using CodeBuilder.Common;
using CodeBuilder.DataFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuilder.BLL
{
    /// <summary>
    /// 实体接口【生产类】
    /// </summary>
    public sealed class CreateIEntity : CreateBase
    {
        public const int Type = 61;

        public const string Name = "IEntity";

        public CreateIEntity()
            : base(CreateIEntity.Name, ".cs")
        {

        }

        public override string CreateMethod(string TableName, string Namespace = "SkeFramework")
        {
            string DataBase = DbFactory.Instance().GetDatabase();
            string DealTableName = ConvertType.ToTitleCase(TableName).Replace("_", "");
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using System.Collections;");
            sb.AppendLine("using System.ComponentModel;");
            sb.AppendLine("");

            sb.AppendLine(string.Format("namespace {0}.Entities", Namespace));
            sb.AppendLine("{");

            sb.AppendLine("    /// <summary>");
            sb.AppendLine("    /// 通用数据实体接口");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine(string.Format("    public interface {0} ", DealTableName));
            sb.AppendLine("  {");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 获取表名");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        string GetTableName();");


            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 获取主键");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        string GetKey();");

            sb.AppendLine("  }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
