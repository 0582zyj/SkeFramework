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
    /// UI层控制器实现【生成类】
    /// </summary>
    public sealed class CreateController : CreateBase
    {
        public const int Type = 11;

        public const string Name = "{0}Controller";

        public CreateController()
            : base(CreateController.Name, ".cs")
        {

        }

        public override string CreateMethod(string TableName, string Namespace = "SkeFramework")
        {
            StringBuilder sb = new StringBuilder();
            string DealTableName = ConvertType.ToTitleCase(TableName).Replace("_", "");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using System.Web;");
            sb.AppendLine("using System.Web.Mvc;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using Newtonsoft.Json.Linq;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine(string.Format("using {0}.Entities.Common;", Namespace));
            sb.AppendLine(string.Format("using {0}.BLL.SHBusiness;", Namespace));
            sb.AppendLine(string.Format("using {0}.Services.DTO.Common;", Namespace));
            sb.AppendLine("");

            sb.AppendLine(string.Format("namespace {0}.UI.WebSite.Controllers", Namespace));
            sb.AppendLine("{");

            sb.AppendLine(string.Format("    public class {0}Controller : Controller", DealTableName));
            sb.AppendLine("   {");

            sb.AppendLine("            #region 页面");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 列表页面");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine(string.Format("        public ActionResult {0}List()", DealTableName));
            sb.AppendLine("        {");
            sb.AppendLine("            return View();");
            sb.AppendLine("        }");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 更新页面");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine(string.Format("        public ActionResult {0}Update(int id)", DealTableName));
            sb.AppendLine("        {");
            sb.AppendLine("            return View();");
            sb.AppendLine("        }");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 新增页面");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine(string.Format("        public ActionResult {0}Add()", DealTableName));
            sb.AppendLine("        {");
            sb.AppendLine("            return View();");
            sb.AppendLine("        }");
            sb.AppendLine("        #endregion ");

            sb.AppendLine("        #region Basic GET POST");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 根据主键ID获取信息");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        [HttpGet]");
            sb.AppendLine(string.Format("        public JsonResult Get{0}Info(int id)", DealTableName));
            sb.AppendLine("        {");
            sb.AppendLine(string.Format("            {0} Info = new {0}();", DealTableName));
            sb.AppendLine(string.Format("            Info = DataHandleManager.Instance().{0}Handle.GetModelByKey(id.ToString());", DealTableName));
            sb.AppendLine("            return Json(Info, JsonRequestBehavior.AllowGet);");
            sb.AppendLine("        }");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 获取列表信息");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        [HttpGet]");
            sb.AppendLine(string.Format("        public JsonResult Get{0}List(int curPage = 1, string keywords = \"\")", DealTableName));
            sb.AppendLine("        {");
            sb.AppendLine(string.Format("            var total = DataHandleManager.Instance().{0}Handle.Count();//取记录数", DealTableName));
            sb.AppendLine("            var pages = new PageDTO(curPage, Convert.ToInt32(total));//初始化分页类");
            sb.AppendLine("            if (curPage == 0)");
            sb.AppendLine("            {");
            sb.AppendLine("                pages.pagesize = Convert.ToInt32(total);");
            sb.AppendLine("            }");
            sb.AppendLine(string.Format("            List<{0}> list = DataHandleManager.Instance().{0}Handle.GetDefaultPagedList(curPage,pages.pagesize).ToList();", DealTableName));
            sb.AppendLine("            var obj = new { pages = pages, dataList = list };//构造对象");
            sb.AppendLine("            return Json(obj, JsonRequestBehavior.AllowGet);");
            sb.AppendLine("        }");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 新增提交方法");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        [HttpPost]");
            sb.AppendLine(string.Format("        public JsonResult {0}Add({0} model)", DealTableName));
            sb.AppendLine("        {");
            sb.AppendLine("            var ResultCode = -1;");
            sb.AppendLine(string.Format("            ResultCode = DataHandleManager.Instance().{0}Handle.Insert(model);", DealTableName));
            sb.AppendLine("            return Json(GetResultMsg(ResultCode>0?100:101), JsonRequestBehavior.AllowGet);");
            sb.AppendLine("        }");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 更新提交方法");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        [HttpPost]");
            sb.AppendLine(string.Format("        public JsonResult {0}Update({0} model)", DealTableName));
            sb.AppendLine("        {");
            sb.AppendLine("            var ResultCode = -1;");
            sb.AppendLine(string.Format("            ResultCode = DataHandleManager.Instance().{0}Handle.Update(model);", DealTableName));
            sb.AppendLine("            return Json(GetResultMsg(ResultCode>0?200:201), JsonRequestBehavior.AllowGet);");
            sb.AppendLine("        }");


            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 删除提交方法");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        [HttpPost]");
            sb.AppendLine(string.Format("        public JsonResult {0}Delete(int id)", DealTableName));
            sb.AppendLine("        {");
            sb.AppendLine("            var ResultCode = -1;");
            sb.AppendLine(string.Format("            ResultCode = DataHandleManager.Instance().{0}Handle.Delete(id);", DealTableName));
            sb.AppendLine("            return Json(GetResultMsg(ResultCode>0?300:301), JsonRequestBehavior.AllowGet);");
            sb.AppendLine("        }");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 根据状态获取结果");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("        public Object GetResultMsg(int ResultCode)");
            sb.AppendLine("        {");
            sb.AppendLine("            string Msg = \"操作成功\";");
            sb.AppendLine("            switch (ResultCode)");
            sb.AppendLine("            {");
            sb.AppendLine("                case -1:");
            sb.AppendLine("                    Msg = \"服务器响应错误\";");
            sb.AppendLine("                    break;");
            sb.AppendLine("                case 100:");
            sb.AppendLine("                    Msg = \"新增成功\";");
            sb.AppendLine("                    break;");
            sb.AppendLine("                case 101:");
            sb.AppendLine("                    Msg = \"新增失败\";");
            sb.AppendLine("                    break;");
            sb.AppendLine("                case 200:");
            sb.AppendLine("                    Msg = \"更新成功\";");
            sb.AppendLine("                    break;");
            sb.AppendLine("                case 201:");
            sb.AppendLine("                    Msg = \"更新失败\";");
            sb.AppendLine("                    break;");
            sb.AppendLine("                case 300:");
            sb.AppendLine("                    Msg = \"删除成功\";");
            sb.AppendLine("                    break;");
            sb.AppendLine("                case 301:");
            sb.AppendLine("                    Msg = \"删除失败\";");
            sb.AppendLine("                    break;");
            sb.AppendLine("                default:");
            sb.AppendLine("                    Msg = \"未知错误\";");
            sb.AppendLine("                    break;");
            sb.AppendLine("            }");
            sb.AppendLine("            var obj = new { ResultCode = ResultCode, ResultMsg = Msg };//构造对象");
            sb.AppendLine("            return obj ;");
            sb.AppendLine("        }");
            sb.AppendLine("        #endregion ");
 
            sb.AppendLine("  }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
