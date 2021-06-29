using Microsoft.AspNetCore.Mvc;
using MicrosServices.API.PermissionSystem.Filters;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Core.Data.Vo;
using SkeFramework.Core.NetworkUtils.Bootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosServices.API.PermissionSystem.Controllers.BaseControllers
{
    public class SkeControllers:ControllerBase
    {
        /// <summary>
        /// 获取当前用户的所有平台列表
        /// </summary>
        /// <returns></returns>
        protected List<long> GetCurrentUserPlatfromNos()
        {
            List<long> platformList = new List<long>();
            OperatorVo operatorVo = HttpContext.Session.Get<OperatorVo>(AuthorizeFilterAttribute.LoginSessionKey);
            if (null == operatorVo)
                return platformList;
            platformList= DataHandleManager.Instance().PsPlatformHandle.GetChildPlatformNoList(operatorVo.platformNo);
            return platformList;
        }
        /// <summary>
        /// 获取当前登录账号
        /// </summary>
        /// <returns></returns>
        protected string GetCurrentUserNo()
        {
            OperatorVo operatorVo = HttpContext.Session.Get<OperatorVo>(AuthorizeFilterAttribute.LoginSessionKey);
            if (null == operatorVo)
                return "";
            return operatorVo.userNo;
        }
    }
}
