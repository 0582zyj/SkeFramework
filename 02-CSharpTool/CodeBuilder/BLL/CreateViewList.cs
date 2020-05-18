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
using SkeFramework.Core.CodeBuilder;

namespace CodeBuilder.BLL
{
    /// <summary>
    /// UI层控制器实现【生成类】
    /// </summary>
    public sealed class CreateViewList : CreateBase
    {
        public const int Type = 52;

        public const string Name = "{0}List";

        public CreateViewList()
            : base(CreateViewList.Name, ".cshtml")
        {

        }

        public override string CreateMethod( string TableName, string Namespace = "SkeFramework")
        {
            StringBuilder sb = new StringBuilder();
            string DealTableName = ConvertType.ToTitleCase(TableName).Replace("_", "");
            string DataBase = DbFactory.Instance().GetDatabase();
            List<ColumnsEntities> columnList = DataHandleManager.repository.GetColumnsList(TableName, DataBase);

            sb.AppendLine("@{");
            sb.AppendLine("    ViewBag.Title = \"列表\";");
            sb.AppendLine("    Layout = \"~/Views/Shared/_Layout.cshtml\";");
            sb.AppendLine("}");

            sb.AppendLine(" <!-- Content Header (Page header) -->");
            sb.AppendLine("<section class=\"content-header\">");
            sb.AppendLine(string.Format("    <h1>{0}列表</h1>",DealTableName));
            sb.AppendLine("    <ol class=\"breadcrumb\">");
            sb.AppendLine("        <li><a href=\"@Url.Action(\"Index\",\"Home\")\"><i class=\"fa fa-home\"></i> 主页</a></li>");
            sb.AppendLine(string.Format("        <li><a href=\"javascript:void(0);\"><i class=\"fa fa-bar-chart\"></i> {0}管理</a></li>", DealTableName));
            sb.AppendLine("    </ol>");
            sb.AppendLine("</section>");


            sb.AppendLine("<!-- Main content -->");
            sb.AppendLine("<section class=\"content\" id=\"section-content\">");
            sb.AppendLine("    <div class=\"row\">");
            sb.AppendLine("        <div class=\"col-md-12\">");
            sb.AppendLine("            <div class=\"box\">");
            sb.AppendLine("                <div class=\"box-body\">");
            sb.AppendLine("                   <div class=\"form-inline\">");
            sb.AppendLine("                       <div class=\"form-group\">");
            sb.AppendLine("                           <input type=\"text\" class=\"form-control\" placeholder=\"关键字\" v-model=\"search.keywords\">");
            sb.AppendLine("                       </div>");
            sb.AppendLine("                       <div class=\"form-group\">");
            sb.AppendLine("                         <button class=\"btn btn-default\" id=\"alert-search\" v-on:click=\"btnSearch\">");
            sb.AppendLine("                            <i class=\"fa fa-search\"></i> 查询");
            sb.AppendLine("                         </button>");
            sb.AppendLine("                       </div>");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                </div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("");

            sb.AppendLine("    <div class=\"row\">");
            sb.AppendLine("        <div class=\"col-md-12\">");
            sb.AppendLine("            <div class=\"box\" >");
            sb.AppendLine("                <!--数据列表页头部-->");
            sb.AppendLine("                <div class=\"box-header with-border\">");
            sb.AppendLine("                    <a class=\"btn btn-default btn-sm\" href=\"javascript:void(0)\" v-on:click=\"toAdd()\">新增</a>");
            sb.AppendLine("                </div>");
            sb.AppendLine("                <!-- /.box-header -->");
            sb.AppendLine("                <div class=\"box-body table-responsive\">");
            sb.AppendLine("                    <table id=\"j-alert-table\" class=\"table table-hover table-bordered datatable\">");
            sb.AppendLine("                        <thead>");
            sb.AppendLine("                            <tr class=\"show-datatable-list-title\">");
            foreach (var item in columnList)
            {
                sb.AppendLine(string.Format("                                <th>{0}</th>", item.COLUMN_COMMENT.Length > 0 ? item.COLUMN_COMMENT : item.COLUMN_NAME));
            }
            sb.AppendLine("                                <th class=\"operation\">操作</th>");
            sb.AppendLine("                            </tr>");
            sb.AppendLine("                        </thead>");
            sb.AppendLine("                        <tbody>");
            sb.AppendLine("                            <tr v-for=\"item in dataList\">");
            foreach (var item in columnList)
            {
                sb.AppendLine("                                <td>{{item." + item.COLUMN_NAME + "}}</td>");
            }
            sb.AppendLine("                                <td class=\"operation\">");
            sb.AppendLine("                                    <a class=\"btn btn-default btn-sm\" href=\"javascript:void(0)\" v-on:click=\"toEdit(item.id)\">修改</a>");
            sb.AppendLine("                                    <a class=\"btn btn-default btn-sm\" href=\"javascript:void(0)\" v-on:click=\"toDelete(item.id)\">删除</a>");
            sb.AppendLine("                                </td>");
            sb.AppendLine("                            </tr>");
            sb.AppendLine("                        </tbody>");
            sb.AppendLine("                        <!--insert tbody here-->");
            sb.AppendLine("                    </table>");
            sb.AppendLine("                </div>");
            sb.AppendLine(string.Format("                <templatepage v-on:getdata=\"GetList\" url=\"@Url.Action(\"Get{0}List\", \"{0}\")\" v-bind:prop=\"prop\"></templatepage>",DealTableName));
            sb.AppendLine("            </div>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <!-- /.box -->");
            sb.AppendLine("    </div>");
            sb.AppendLine("</section>");
            sb.AppendLine("<!-- /.content -->");

            sb.AppendLine("<script>");
            sb.AppendLine("        var vm = new Vue({");
            sb.AppendLine("            el: \"#section-content\",");
            sb.AppendLine("            data: {");
            sb.AppendLine(string.Format("                updateJumpUrl: \"@Url.Action(\"{0}Update\", \"{0}\")\",",DealTableName));
            sb.AppendLine(string.Format("                addJumpUrl: \"@Url.Action(\"{0}Add\", \"{0}\")\",",DealTableName));
            sb.AppendLine(string.Format("                deleteUrl: \"@Url.Action(\"{0}Delete\", \"{0}\")\",", DealTableName));
            sb.AppendLine(string.Format("                GetListUrl: \"@Url.Action(\"Get{0}List\", \"{0}\")\",", DealTableName));
            sb.AppendLine("                dataList: [],");
            sb.AppendLine("                search: { keywords: \"\"}");
            sb.AppendLine("            },");
            sb.AppendLine("            computed: {");
            sb.AppendLine("                prop: function () {");
            sb.AppendLine("                    return { keywords: this.search.keywords }");
            sb.AppendLine("                }");
            sb.AppendLine("            },");
            sb.AppendLine("            methods: {");
            sb.AppendLine("                GetList: function (data) {");
            sb.AppendLine("                    this.dataList = data;");
            sb.AppendLine("                },");
            sb.AppendLine("                toAdd: function () {");
            sb.AppendLine("                    window.location.href = this.addJumpUrl;");
            sb.AppendLine("                },");
            sb.AppendLine("                toEdit: function (_id) {");
            sb.AppendLine("                    window.location.href = this.updateJumpUrl + '?id=' + _id;");
            sb.AppendLine("                },");
            sb.AppendLine("                toDelete: function (_id) {");
            sb.AppendLine("                    axios.post(this.deleteUrl, { id: _id })");
            sb.AppendLine("                     .then(function (response) {");
            sb.AppendLine("                         if (response.status == 200) {");
            sb.AppendLine("                             alert(response.data.msg);");
            sb.AppendLine("                             if (response.data.code == 200) {");
            sb.AppendLine("                                 window.location.reload();");
            sb.AppendLine("                             }");
            sb.AppendLine("                         }");
            sb.AppendLine("                     })");
            sb.AppendLine("                     .catch(function (error) {");
            sb.AppendLine("                         console.log(error);");
            sb.AppendLine("                     });");
            sb.AppendLine("                },");
            sb.AppendLine("                btnSearch: function () {");
            sb.AppendLine("                    if (this.$children.length > 0) {");
            sb.AppendLine("                        this.$children[0].go(1);");
            sb.AppendLine("                    }");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("        });");
            sb.AppendLine("</script>");
            sb.AppendLine("");
            return sb.ToString();
        }
    }
}
