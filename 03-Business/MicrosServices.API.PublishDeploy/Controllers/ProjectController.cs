using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common.PublishDeploy;
using MicrosServices.Helper.Core.Common;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;
using SkeFramework.Core.SnowFlake;

namespace MicrosServices.API.PublishDeploy.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        #region 基础查询
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetList(string keywords = "")
        {
            Expression<Func<PdProject, bool>> where = null;
            if (!String.IsNullOrEmpty(keywords))
            {
                where = (o => o.Name.Contains(keywords));
            }
            List<PdProject> list = DataHandleManager.Instance().PdProjectHandle.GetList(where).ToList();
            return new JsonResponses(list);
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetPageList([FromQuery]PageModel page, [FromQuery] string keywords = "")
        {
            try
            {
                Expression<Func<PdProject, bool>> where = null;
                if (!String.IsNullOrEmpty(keywords))
                {
                    where = (o => o.Name.Contains(keywords));
                }
                page.setTotalCount(Convert.ToInt32(DataHandleManager.Instance().PdProjectHandle.Count(where)));//取记录数
                List<PdProject> list = DataHandleManager.Instance().PdProjectHandle.GetDefaultPagedList(page.PageIndex, page.PageSize, where).ToList();
                PageResponse<PdProject> response = new PageResponse<PdProject>
                {
                    page = page,
                    dataList = list
                };
                return new JsonResponses(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 根据主键ID获取信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetInfo(int id)
        {
            PdProject Info = new PdProject();
            Info = DataHandleManager.Instance().PdProjectHandle.GetModelByKey(id.ToString());
            return new JsonResponses(Info);
        }

        #endregion

        #region 增删改
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Create([FromForm] PdProject project)
        {
            try
            {
                //bool checkResult = true;
                project.ProjectNo = AutoIDWorker.Example.GetAutoSequence();
                project.InputTime = DateTime.Now;
                int result = DataHandleManager.Instance().PdProjectHandle.Insert(project);
                if (result > 0)
                {
                    return JsonResponses.Success;
                }

                return JsonResponses.Failed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 删除提交方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Delete([FromForm] int id)
        {
            int result = DataHandleManager.Instance().PdProjectHandle.Delete(id);
            if (result > 0)
            {
                return JsonResponses.Success;
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Update([FromForm] PdProject project)
        {
            try
            {
                project.UpdateTime = DateTime.Now;
                int result = DataHandleManager.Instance().PdProjectHandle.Update(project);
                if (result > 0)
                {
                    return JsonResponses.Success;
                }
                return JsonResponses.Failed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetOptionValues()
        {
            List<OptionValue> optionValues = DataHandleManager.Instance().PdProjectHandle.GetOptionValues();
            return new JsonResponses(optionValues);
        }
        #endregion

    }
}