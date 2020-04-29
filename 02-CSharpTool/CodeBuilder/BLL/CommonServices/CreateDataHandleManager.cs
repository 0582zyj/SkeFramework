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
    /// 数据管理中心
    /// </summary>
    public sealed class CreateDataHandleManager : CreateBase
    {
        public const int Type = 62;

        public const string Name = "DataHandleManager";

        public CreateDataHandleManager()
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
            sb.AppendLine("using SkeFramework.DataBase.DataAccess.DataHandle.Achieve;");
            sb.AppendLine(String.Format("using {0}.BLL.Business.Achieve;", Namespace));
            sb.AppendLine(String.Format("using {0}.Entities.Common;", Namespace));
            sb.AppendLine("");

            sb.AppendLine(String.Format("namespace {0}.BLL.Business", Namespace));
            sb.AppendLine("{");

            sb.AppendLine("    /// <summary>");
            sb.AppendLine("    /// 数据中心代理");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine(String.Format("    public class {0} ", Name));
            sb.AppendLine("  {");

            sb.AppendLine(String.Format("        private static {0} _manager;", Name));
            sb.AppendLine("");
            sb.AppendLine(String.Format("        public static {0} Instance()", Name));
            sb.AppendLine("        {");
            sb.AppendLine("            if (_manager == null)");
            sb.AppendLine("            {");
            sb.AppendLine("                DataHandleFactory.SetDataHandleFactory(new DALDataHandleFactory());");
            sb.AppendLine("                _manager = new DataHandleManagers();");
            sb.AppendLine("            }");
            sb.AppendLine(String.Format("            return _manager ?? (_manager = new {0}()); ", Name));
            sb.AppendLine("        }");

            sb.AppendLine(String.Format("        public I{0}Handle {0}Handle", DealTableName));
            sb.AppendLine("        {");
            sb.AppendLine("            get ");
            sb.AppendLine("            {");
            sb.AppendLine(String.Format(@"                return DataHandleFactory.GetDataHandle<{0}Handle, {0}>(); ", DealTableName));
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            return sb.ToString();
        }
    }
}