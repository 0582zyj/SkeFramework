using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;
using SkeFramework.Core.SnowFlake;

namespace MicrosServices.API.PermissionSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        #region Basic GET POST
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetList(string keywords = "")
        {
            Expression<Func<PsPlatform, bool>> where = null;
            if (!String.IsNullOrEmpty(keywords))
            {
                where = (o => o.Name.Contains(keywords));
            }
            List<PsPlatform> list = DataHandleManager.Instance().PsPlatformHandle.GetList(where).ToList();
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
                Expression<Func<PsPlatform, bool>> where = null;
                if (!String.IsNullOrEmpty(keywords))
                {
                    where = (o => o.Name.Contains(keywords));
                }
                int total = Convert.ToInt32(DataHandleManager.Instance().PsPlatformHandle.Count(where));//取记录数
                List<PsPlatform> list = DataHandleManager.Instance().PsPlatformHandle.GetDefaultPagedList(page.PageIndex, page.PageSize, where).ToList();
                PageResponse<PsPlatform> response = new PageResponse<PsPlatform>
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
        public ActionResult<JsonResponses> GetPsPlatformInfo(int id)
        {
            PsPlatform Info = new PsPlatform();
            Info = DataHandleManager.Instance().PsPlatformHandle.GetModelByKey(id.ToString());
            return new JsonResponses(Info);
        }
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Add([FromForm] PsPlatform platform)
        {
            try
            {
                //bool checkResult = true;
                platform.InputTime = DateTime.Now;
                platform.PlatformNo = AutoIDWorker.Example.GetAutoSequence();
                int result = DataHandleManager.Instance().PsPlatformHandle.Insert(platform);
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
            int result = DataHandleManager.Instance().PsMenuHandle.Delete(id);
            if (result > 0)
            {
                return JsonResponses.Success;
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Update([FromForm] PsPlatform platform)
        {
            try
            {
                //bool checkResult = true;
                platform.UpdateTime = DateTime.Now;
                int result = DataHandleManager.Instance().PsPlatformHandle.Update(platform);
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
    }
}