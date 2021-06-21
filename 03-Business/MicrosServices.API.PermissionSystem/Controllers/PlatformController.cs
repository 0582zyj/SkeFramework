using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.UserCenter.FORM;
using MicrosServices.Helper.DataUtil.Tree;
using MicrosServices.SDK.UserCenter;
using Newtonsoft.Json;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;
using SkeFramework.Core.SnowFlake;

namespace MicrosServices.API.PermissionSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private UserSDK userSDK = new UserSDK();

        #region 基础查询
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
                page.setTotalCount(Convert.ToInt32(DataHandleManager.Instance().PsPlatformHandle.Count(where)));//取记录数
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
        public ActionResult<JsonResponses> GetInfo(long id)
        {
            PsPlatform Info = new PsPlatform();
            Info = DataHandleManager.Instance().PsPlatformHandle.GetModelByKey(id.ToString());
            return new JsonResponses(Info);
        }
      
        #endregion

        #region 增删改
        /// <summary>
        /// 新增平台
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> Create([FromForm] PsPlatform platform)
        {
            try
            {
                //bool checkResult = true;
                platform.InputTime = DateTime.Now;
                platform.PlatformNo = AutoIDWorker.Example.GetAutoSequence();
                PsPlatform ParentInfo = DataHandleManager.Instance().PsPlatformHandle.GetModelByKey(platform.ParentNo.ToString());
                platform.TreeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsPlatform>(ParentInfo, platform.ParentNo);
                UcUsers users = this.userSDK.GetUserInfo(Convert.ToInt32(platform.DefaultUserNo));
                if (users != null)
                    return new JsonResponses("用户已存在");
                RegisterPlatformForm registerPlatformForm = new RegisterPlatformForm();
                registerPlatformForm.UserNo= platform.DefaultUserNo;
                registerPlatformForm.UserName = platform.DefaultUserName;
                registerPlatformForm.PlatformNo = platform.PlatformNo;
                registerPlatformForm.Password = ConstData.DEFAULT_PASSWORD;
                registerPlatformForm.Phone = "";
                registerPlatformForm.Email = "";
                registerPlatformForm.InputUser = platform.InputUser;
                JsonResponses jsonResponses = userSDK.RegisterPlatfrom(registerPlatformForm);
                if (!jsonResponses.ValidateResponses())
                    return jsonResponses;
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
            long count = DataHandleManager.Instance().PsManagementHandle.Count();
            if (count > 0)
            {
                return new JsonResponses("当前平台信息不为空，暂不支持删除");
            }
            int result = DataHandleManager.Instance().PsPlatformHandle.Delete(id);
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
                int result = DataHandleManager.Instance().PsPlatformHandle.PlatformUpdate(current);
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
        public ActionResult<JsonResponses> GetOptionValues([FromForm] long PlatformNo = ConstData.DefaultNo)
        {
            List<OptionValue> optionValues = DataHandleManager.Instance().PsPlatformHandle.GetOptionValues( PlatformNo);
            return new JsonResponses(optionValues);
        }
        #endregion
    }
}