using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.API.UserCenter.Filters;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common;
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Helper.Core.UserCenter.FORM;
using SkeFramework.Core.Encrypts;
using SkeFramework.Core.Network.DataUtility;
using SkeFramework.Core.Network.Responses;

namespace MicrosServices.API.UserCenter.Controllers
{

    [Route("api/user")]
    [ApiController]
    public class UserWebController : ControllerBase
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public ActionResult<JsonResponses> GetUserInfo([FromQuery]string userNo)
        {
            try
            {
                UcUsers users = DataHandleManager.Instance().UcUsersHandle.GetUsersInfo(userNo);
                return new JsonResponses(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("pageList")]
        public ActionResult<JsonResponses> GetPageList([FromQuery]PageModel page, [FromQuery] string keywords = "")
        {
            try
            {
                Expression<Func<UcUsers, bool>> where = null;
                if (!String.IsNullOrEmpty(keywords))
                {
                    where = (o => o.UserNo.Contains(keywords));
                }
                int total = Convert.ToInt32(DataHandleManager.Instance().UcUsersHandle.Count(where));//取记录数
                List<UcUsers> list = DataHandleManager.Instance().UcUsersHandle.GetDefaultPagedList(page.PageIndex, page.PageSize, where).ToList();
                PageResponse<UcUsers> response = new PageResponse<UcUsers>
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
        /// 平台用户注册接口
        /// </summary>
        /// <param name="registerPlatform"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("registerPlatform")]
        [AuthorizeFilterAttribute(1)]
        public ActionResult<JsonResponses> RegisterPlatform([FromForm]RegisterPlatformForm registerPlatform)
        {
            string MdfPas = MD5Helper.GetMD5String(registerPlatform.Password);
            registerPlatform.Password = MdfPas;
            if(String.IsNullOrEmpty(registerPlatform.Phone))
            {
                registerPlatform.Phone = ConstData.DefaultNo.ToString();
            }
            if (String.IsNullOrEmpty(registerPlatform.Email))
            {
                registerPlatform.Email = ConstData.DefaultNo.ToString();
            }
            LoginResultType LoginResult = DataHandleManager.Instance().UcUsersHandle.RegisterPlatform(registerPlatform);
            if (LoginResult == LoginResultType.SUCCESS_REGISTOR)
            {
                return new JsonResponses(JsonResponses.SuccessCode, LoginResult.ToString(), registerPlatform);
            }
            return new JsonResponses(JsonResponses.FailedCode, LoginResult.ToString(), LoginResult);
        }
        /// <summary>
        /// 平台用户注销
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("cancelPlatform")]
        [AuthorizeFilterAttribute(1)]
        public ActionResult<JsonResponses> CancelPlatform([FromForm]string UserNo)
        {
            if (!DataHandleManager.Instance().UcUsersHandle.CheckUserNoIsExist(UserNo))
            {
                return new JsonResponses(JsonResponses.FailedCode, LoginResultType.ERROR_USER_NOT_EXIST.ToString()
                    , LoginResultType.ERROR_USER_NOT_EXIST); 
            }
            LoginResultType LoginResult = DataHandleManager.Instance().UcUsersHandle.CancelPlatform(UserNo);
            if (LoginResult == LoginResultType.SUCCESS_CANCEL)
            {
                return new JsonResponses(JsonResponses.SuccessCode, LoginResult.ToString(), LoginResult);
            }
            return new JsonResponses(JsonResponses.FailedCode, LoginResult.ToString(), LoginResult);
        }
    }
}