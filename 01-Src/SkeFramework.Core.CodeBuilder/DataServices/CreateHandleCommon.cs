using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Core.CodeBuilder.DataServices.Achieve;
using SkeFramework.Core.CodeBuilder.DataServices.Interfaces;
using SkeFramework.Core.CodeBuilder.DataCommon;

namespace SkeFramework.Core.CodeBuilder.DataServices
{
    /// <summary>
    /// 数据层通用实现【生成类】
    /// </summary>
    public sealed class CreateHandleCommon : CreateBase
    {
        public const int Type = 32;

        public const string Name = "{0}HandleCommon";

        public CreateHandleCommon()
            : base(CreateHandleCommon.Name, ".cs")
        {

        }


        public override string CreateMethod( string TableName, string Namespace = "SkeFramework")
        {
      
            StringBuilder sb = new StringBuilder();
            string DealTableName = ConvertType.ToTitleCase(TableName).Replace("_", "");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using System.Collections;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using ULCSharp.DAL.Common.DataFactory;");
            sb.AppendLine("using ULCSharp.DAL.Common.DataCommon;");
            sb.AppendLine(string.Format("using {0}.DAL.Interfaces;", namespace_dll));
            sb.AppendLine(string.Format("using {0}.DAL.DataAccess.DataHandle.Common;", namespace_dll));
            sb.AppendLine(string.Format("using {0}.Entities.Common;", Namespace));
            sb.AppendLine("");
 
            sb.AppendLine(string.Format("namespace {0}.DAL.DataAccess.DataHandle.Repositorys", Namespace));
            sb.AppendLine("{");

            sb.AppendLine(string.Format("    public class {0}HandleCommon : DataTableHandle<{0}>, I{0}HandleCommon", DealTableName));
            sb.AppendLine("  {");

            sb.AppendLine(string.Format("        public {0}HandleCommon(IRepository<{0}> dataSerialer)", DealTableName));
            sb.AppendLine(string.Format("            : base(dataSerialer, {0}.TableName, false)", DealTableName));
            sb.AppendLine("        {");
            sb.AppendLine("        }");
            sb.AppendLine("  }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
