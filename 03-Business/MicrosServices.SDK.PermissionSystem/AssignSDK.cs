using MicrosServices.Helper.Core.Form;
using MicrosServices.Helper.Core.Form.AssignForm;
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
        private static readonly string GetUserOrgAssignUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/getUserOrgAssign";
        private static readonly string CreateUserOrgsUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/createUserOrgs";
        private static readonly string GetRolesAssignUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/getRolesAssign";
        private static readonly string CreateUserRolesUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/createUserRoles";
        private static readonly string GetManagementAssignUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/getManagementAssign";
        private static readonly string CreateManagementRolesUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/createManagementRoles";
        private static readonly string GetMenuAssignUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/getMenuAssign";
        private static readonly string CreateManagementMenusUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/createManagementMenus";
        private static readonly string GetOrgAssignUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/getOrgAssign";
        private static readonly string CreateOrgRolesUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/createOrgRoles";
        private static readonly string GetMenuManagmentAssignUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/getMenuManagmentAssign";
        private static readonly string CreateMenuManagementsUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/createMenuManagements";

        #region 用户角色
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public JsonResponses GetRolesAssign(long UserNo)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetRolesAssignUrl;
                request.SetValue("userNo", UserNo);
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
        /// <param name="userNo"></param>
        /// <returns></returns>
        public JsonResponses GetUserOrgAssign(string UserNo)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetUserOrgAssignUrl;
                request.SetValue("userNo",Convert.ToInt64( UserNo));
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
        public JsonResponses GetManagementAssign(long RolesNo, long ManagementType)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetManagementAssignUrl;
                request.SetValue("rolesNo", RolesNo);
                request.SetValue("managementType", ManagementType); 
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
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = CreateManagementRolesUrl;
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
        /// <param name="managementNo"></param>
        /// <returns></returns>
        public JsonResponses GetMenuAssign(long ManagementNo)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetMenuAssignUrl;
                request.SetValue("managementNo", ManagementNo);
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
        /// <param name="userNo"></param>
        /// <returns></returns>
        public JsonResponses GetOrgAssign(long OrgNo)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetOrgAssignUrl;
                request.SetValue("orgNo", OrgNo);
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

        #region 菜单权限
        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name="menuNo"></param>
        /// <returns></returns>
        public JsonResponses GetMenuManagmentAssign(long MenuNo)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetMenuManagmentAssignUrl;
                request.SetValue("menuNo", MenuNo);
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
        public JsonResponses CreateMenuManagements(MenuManagementsForm model)
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = CreateMenuManagementsUrl
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
