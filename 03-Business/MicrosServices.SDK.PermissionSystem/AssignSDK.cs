using MicrosServices.Helper.Core.Form;
using Newtonsoft.Json;
using SkeFramework.Core.Network.Enums;
using SkeFramework.Core.Network.Https;
using SkeFramework.Core.Network.Requests;
using SkeFramework.Core.Network.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.SDK.PermissionSystem
{
   public class AssignSDK
    {
        private static readonly string GetUserOrgAssignUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Assign/GetUserOrgAssign";
        private static readonly string CreateUserOrgsUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Assign/CreateUserOrgs";
        private static readonly string GetRolesAssignUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Assign/GetRolesAssign";
        private static readonly string CreateUserRolesUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Assign/CreateUserRoles";
        private static readonly string GetManagementAssignUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Assign/GetManagementAssign";
        private static readonly string CreateManagementRolesUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Assign/CreateManagementRoles";
        private static readonly string GetMenuAssignUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Assign/GetMenuAssign";
        private static readonly string CreateManagementMenusUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Assign/CreateManagementMenus";
        private static readonly string GetOrgAssignUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Assign/GetOrgAssign";
        private static readonly string CreateOrgRolesUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Assign/CreateOrgRoles";

        #region 用户角色
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        public JsonResponses GetRolesAssign(long UserNo)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetRolesAssignUrl
                };
                request.SetValue("UserNo", UserNo);
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResponses CreateUserRoles(UserRolesForm model)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = CreateUserRolesUrl
                };
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = JsonConvert.SerializeObject(model),
                    Method = RequestTypeEnums.POST_JSON
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        #endregion

        #region 机构角色
        /// <summary>
        /// 获取机构角色列表
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        public JsonResponses GetUserOrgAssign(string UserNo)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetUserOrgAssignUrl
                };
                request.SetValue("UserNo",Convert.ToInt64( UserNo));
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 机构角色授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResponses CreateUserOrgs(UserOrgsForm model)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = CreateUserOrgsUrl
                };
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = JsonConvert.SerializeObject(model),
                    Method = RequestTypeEnums.POST_JSON
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        #endregion

        #region 权限角色
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="RoleNo"></param>
        /// <returns></returns>
        public JsonResponses GetManagementAssign(long RolesNo)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetManagementAssignUrl
                };
                request.SetValue("RolesNo", RolesNo);
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 权限系统
        /// </summary>
        /// <returns></returns>
        public JsonResponses CreateManagementRoles(ManagementRolesForm managementRolesForm)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = CreateManagementRolesUrl
                };
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = JsonConvert.SerializeObject(managementRolesForm),
                    Method = RequestTypeEnums.POST_JSON
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        #endregion

        #region 权限菜单
        /// <summary>
        /// 获取权限菜单
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        public JsonResponses GetMenuAssign(long ManagementNo)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetMenuAssignUrl
                };
                request.SetValue("ManagementNo", ManagementNo);
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }

        /// <summary>
        /// 权限菜单授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResponses CreateManagementMenus(ManagementMenusForm model)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = CreateManagementMenusUrl
                };
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = JsonConvert.SerializeObject(model),
                    Method = RequestTypeEnums.POST_JSON
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }

        #endregion

        #region 机构角色
        /// <summary>
        /// 获取机构角色列表
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        public JsonResponses GetOrgAssign(long OrgNo)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetOrgAssignUrl
                };
                request.SetValue("OrgNo", OrgNo);
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 机构角色授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResponses CreateOrgRoles(OrgRolesForm model)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = CreateOrgRolesUrl
                };
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = JsonConvert.SerializeObject(model),
                    Method = RequestTypeEnums.POST_JSON
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
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
