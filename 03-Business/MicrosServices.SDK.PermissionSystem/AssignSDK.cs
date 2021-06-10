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

        private static readonly string GetGroupManagmentsAssignUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/getGroupManagmentsAssign";
        private static readonly string CreateGroupManagmentsUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/assign/createGroupManagments";
        
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
                string result = HttpHelper.Example.GetWebData(request);
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
                RequestBase request = RequestBase.PostJson as RequestBase;
                request.Url = CreateUserRolesUrl;
                request.SetJsonValue(model);
                string result = HttpHelper.Example.GetWebData(request);
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        #endregion

        #region 用户机构
        /// <summary>
        /// 获取用户机构列表
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
                string result = HttpHelper.Example.GetWebData(request);
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 用户机构授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResponses CreateUserOrgs(UserOrgsForm model)
        {
            try
            {
                RequestBase request = RequestBase.PostJson as RequestBase;
                request.Url = CreateUserOrgsUrl;
                request.SetJsonValue(model);
                string result = HttpHelper.Example.GetWebData(request);
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
        public JsonResponses GetManagementAssign(long RolesNo, List<int> ManagementTypeList)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetManagementAssignUrl;
                request.SetValue("rolesNo", RolesNo);
                request.SetValue("managementType", ManagementTypeList); 
                string result = HttpHelper.Example.GetWebData(request);
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
                RequestBase request = RequestBase.PostJson as RequestBase;
                request.Url = CreateManagementRolesUrl;
                request.SetJsonValue(managementRolesForm);
             
                string result = HttpHelper.Example.GetWebData(request);
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
                string result = HttpHelper.Example.GetWebData(request);
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
                RequestBase request = RequestBase.PostJson as RequestBase;
                request.Url = CreateOrgRolesUrl;
                request.SetJsonValue(model);
                string result = HttpHelper.Example.GetWebData(request);
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
                string result = HttpHelper.Example.GetWebData(request);
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
                RequestBase request = RequestBase.PostJson as RequestBase;
                request.Url = CreateMenuManagementsUrl;
                request.SetJsonValue(model);
                string result = HttpHelper.Example.GetWebData(request);
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
                string result = HttpHelper.Example.GetWebData(request);
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
                RequestBase request = RequestBase.PostJson as RequestBase;
                request.Url = CreateManagementMenusUrl;
                request.SetJsonValue(model);
                string result = HttpHelper.Example.GetWebData(request);
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }

        #endregion

        #region 分组权限
        /// <summary>
        /// 获取分组权限列表
        /// </summary>
        /// <param name="managementNo"></param>
        /// <param name="gROUP_TYPE"></param>
        /// <returns></returns>
        public JsonResponses GetGroupManagmentsAssign(long managementNo)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetGroupManagmentsAssignUrl;
                request.SetValue("managementNo", managementNo);
                string result = HttpHelper.Example.GetWebData(request);
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }

        /// <summary>
        /// 分组权限列表授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResponses CreateGroupManagments(GroupManagementsForm model)
        {
            try
            {
                RequestBase request = RequestBase.PostJson as RequestBase;
                request.Url = CreateGroupManagmentsUrl;
                request.SetJsonValue(model);
                string result = HttpHelper.Example.GetWebData(request);
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
