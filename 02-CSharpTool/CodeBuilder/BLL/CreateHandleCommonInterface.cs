using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBuilder.BLL.Achieve;
using CodeBuilder.BLL.Interfaces;
using CodeBuilder.Common;

namespace CodeBuilder.BLL
{
    /// <summary>
    /// 数据层通用实现接口【生成类】
    /// </summary>
    public sealed class CreateHandleCommonInterface : CreateBase
    {
        public const int Type = 31;

        public const string Name = "I{0}HandleCommon";

    
        public CreateHandleCommonInterface()
            : base(CreateHandleCommonInterface.Name, ".cs")
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
            sb.AppendLine(string.Format("using {0}.DAL.Interfaces;", namespace_dll));
            sb.AppendLine(string.Format("using {0}.Entities.Common;", Namespace));
            sb.AppendLine("");
 
            sb.AppendLine(string.Format("namespace {0}.DAL.DataAccess.DataHandle.Repositorys", Namespace));
            sb.AppendLine("{");

            sb.AppendLine(string.Format("    public interface I{0}HandleCommon : IDataTableHandle<{0}>", DealTableName));
            sb.AppendLine("  {");
            
            sb.AppendLine("  }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
