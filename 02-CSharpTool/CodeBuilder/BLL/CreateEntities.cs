using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBuilder.BLL.Achieve;
using CodeBuilder.BLL.Interfaces;
using CodeBuilder.Common;
using CodeBuilder.DAL.Repositorys;
using CodeBuilder.DataFactory;
using CodeBuilder.Model.Entities;

namespace CodeBuilder.BLL
{
    /// <summary>
    /// 仓库实体【生成类】
    /// </summary>
    public sealed class CreateEntities : CreateBase
    {
        public const int Type = 6;

        public const string Name = "{0}";

        public CreateEntities()
            : base(CreateEntities.Name, ".cs")
        {

        }

        public override string CreateMethod( string TableName, string Namespace = "SkeFramework")
        {
            string DataBase = DbFactory.Instance().GetDatabase();
            List<ColumnsEntities> columnList = DataHandleManager.repository.GetColumnsList(TableName, DataBase);
            string DealTableName = ConvertType.ToTitleCase(TableName).Replace("_", "");
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using System.Collections;");
            sb.AppendLine("using System.ComponentModel;");
            sb.AppendLine(string.Format("using {0}.DAL.Common.AttributeExtend;", namespace_dll));
            sb.AppendLine("");

            sb.AppendLine(string.Format("namespace {0}.Entities.Common", Namespace));
            sb.AppendLine("{");

            sb.AppendLine(string.Format("    public class {0} : IEntity", DealTableName));
            sb.AppendLine("  {");
            sb.AppendLine(string.Format("            public const string TableName = \"{0}\";", TableName));
            //获取表中的列名和数据类型
            foreach (var item in columnList)
            {
                if (item.COLUMN_NAME.ToLower().Equals("id"))
                {
                    sb.AppendLine("        [KeyAttribute(true)]");

                }
                string NetType = ConvertType.ToNetType(item.DATA_TYPE);

                sb.AppendLine(string.Format("        [Description(\"{0}\")]", item.COLUMN_COMMENT));
                sb.AppendLine("        public "+NetType+" "+ item.COLUMN_NAME+" { get; set; }");
            }

            sb.AppendLine("       public string GetTableName()");
            sb.AppendLine("       {");
            sb.AppendLine("           return TableName;");
            sb.AppendLine("       }");

            sb.AppendLine("       public string GetKey()");
            sb.AppendLine("       {");
            sb.AppendLine("           return \"id\";");
            sb.AppendLine("       }");

            sb.AppendLine("  }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
