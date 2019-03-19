using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// UI层控制器实现【生成类】
    /// </summary>
    public sealed class CreateViewAdd : CreateBase
    {
        public const int Type = 13;

        public const string Name = "{0}Add";

        public CreateViewAdd()
            : base(CreateViewAdd.Name, ".cshtml")
        {

        }

        public override string CreateMethod( string TableName, string Namespace = "SkeFramework")
        {
            StringBuilder sb = new StringBuilder();
            string DealTableName = ConvertType.ToTitleCase(TableName).Replace("_", "");
            string DataBase = DbFactory.Instance().GetDatabase();
            List<ColumnsEntities> columnList = DataHandleManager.repository.GetColumnsList(TableName, DataBase);

            sb.AppendLine("@{");
            sb.AppendLine("    ViewBag.Title = \"新增\";");
            sb.AppendLine("    Layout = \"~/Views/Shared/_Layout.cshtml\";");
            sb.AppendLine("}");

            sb.AppendLine(" <!-- Content Header (Page header) -->");
            sb.AppendLine("<section class=\"content-header\">");
            sb.AppendLine(string.Format("    <h1>{0}新增</h1>", DealTableName));
            sb.AppendLine("    <ol class=\"breadcrumb\">");
            sb.AppendLine("        <li><a href=\"@Url.Action(\"Index\",\"Home\")\"><i class=\"fa fa-home\"></i> 主页</a></li>");
            sb.AppendLine(string.Format("        <li><a href=\"@Url.Action(\"{0}List\",\"{0}\")\"><i class=\"fa fa-bar-chart\"></i> {0}管理</a></li>", DealTableName));
            sb.AppendLine("    </ol>");
            sb.AppendLine("</section>");


            sb.AppendLine("<!-- Main content -->");
            sb.AppendLine("<section class=\"content\" id=\"section-content\">");
            sb.AppendLine("    <div class=\"row\">");
            sb.AppendLine("        <div class=\"col-md-12\">");
            sb.AppendLine("            <div class=\"nav-tabs-custom\">");
            sb.AppendLine("                <ul class=\"nav nav-tabs\">");
            sb.AppendLine("                    <li class=\"active\"><a href=\"#profile\" data-toggle=\"tab\" aria-expanded=\"true\">基本信息</a></li>");
            sb.AppendLine("                </ul>");
            sb.AppendLine("                <div class=\"tab-content\">");
            sb.AppendLine("                    <div class=\"tab-pane active\" id=\"profile\">");
            sb.AppendLine("                        <form class=\"form-horizontal\">");


            foreach (var item in columnList)
            {
                string Comment = item.COLUMN_COMMENT.Length > 0 ? item.COLUMN_COMMENT : item.COLUMN_NAME;
                int length=string.IsNullOrEmpty( item.CHARACTER_MAXIMUM_LENGTH)?200:Convert.ToInt32( item.CHARACTER_MAXIMUM_LENGTH);
                sb.AppendLine("                            <div class=\"form-group\">");
                sb.AppendLine(string.Format("                                <label for=\"{0}\" class=\"col-sm-2 control-label\">{1}</label>", item.COLUMN_NAME, Comment));
                sb.AppendLine("                                <div class=\"col-sm-10\">");
                sb.AppendLine(string.Format("                                    <input type=\"text\" class=\"form-control\" name=\"{0}\" id=\"{0}\" v-model=\"dataItem.{0}\" required maxlength=\"{1}\" placeholder=\"{2}\">",item.COLUMN_NAME,length,Comment));
                sb.AppendLine("                                </div>");
                sb.AppendLine("                            </div>");
            }
            sb.AppendLine("                            <input type=\"hidden\" name=\"token\" value=\"5cbd303f221a626ce0f404cec31b4ea4\">");
        
            sb.AppendLine("                            <div class=\"form-group\">");
            sb.AppendLine("                                <div class=\"col-sm-offset-2 col-sm-10\">");
            sb.AppendLine("                                    <button type=\"button\" class=\"btn btn-default\" v-on:click=\"submit()\">保存</button>");
            sb.AppendLine("                                </div>");
            sb.AppendLine("                            </div>");
            sb.AppendLine("                        </form>");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                    <!-- /.tab-pane -->");
            sb.AppendLine("                </div>");
            sb.AppendLine("                <!-- /.tab-content -->");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <!-- /.nav-tabs-custom -->");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("</section>");

            sb.AppendLine("<script>");
            sb.AppendLine("        var vm = new Vue({");
            sb.AppendLine("            el: \"#section-content\",");
            sb.AppendLine("            data: {");
            sb.AppendLine(string.Format("                listJumpUrl: \"@Url.Action(\"{0}List\", \"{0}\")\",", DealTableName));
            sb.AppendLine(string.Format("                addPostUrl: \"@Url.Action(\"{0}Add\", \"{0}\")\",", DealTableName));
            sb.AppendLine("                dataItem: {},");
            sb.AppendLine("            },");
            sb.AppendLine("            created: function () {");
            sb.AppendLine("            },");
            sb.AppendLine("            methods: {");
            sb.AppendLine("                submit: function () {");
            sb.AppendLine("                    var _self = this;");
            sb.AppendLine("                    axios.post(this.addPostUrl, this.dataItem)");
            sb.AppendLine("                     .then(function (response) {");
            sb.AppendLine("                         if (response.status == 200) {");
            sb.AppendLine("                             alert(response.data.ResultMsg);");
            sb.AppendLine("                             if (response.data.ResultCode == 100) {");
            sb.AppendLine("                                 window.location.href = _self.listJumpUrl;");
            sb.AppendLine("                             }");
            sb.AppendLine("                         }");
            sb.AppendLine("                     })");
            sb.AppendLine("                     .catch(function (error) {");
            sb.AppendLine("                         console.log(error);");
            sb.AppendLine("                     });");
            sb.AppendLine("                },");
            sb.AppendLine("            }");
            sb.AppendLine("        });");
            sb.AppendLine("</script>");
            sb.AppendLine("");
            return sb.ToString();
        }
    }
}
