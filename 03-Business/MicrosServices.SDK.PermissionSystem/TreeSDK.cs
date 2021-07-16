using MicrosServices.Entities.Common;
using Newtonsoft.Json;
using SkeFramework.Core.Network.DataUtility;
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
    public class TreeSDK
    {
        private readonly string GetMenuTreeListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/tree/getMenuTreeList";
        private readonly string GetManagementTreeListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/tree/getManagementTreeList";
        private readonly string GetOrganizationTreeListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/tree/getOrganizationTreeList";
        private readonly string GetRolesTreeListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/tree/getRolesTreeList";

        private SdkUtil sdkUtil = new SdkUtil();

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<TreeNodeInfo> GetMenuTreeList(long PlatformNo)
        {
            return GetTreeListByPlatform(PlatformNo, GetMenuTreeListUrl);
        }
        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<TreeNodeInfo> GetManagementTreeList(long PlatformNo)
        {
            return GetTreeListByPlatform(PlatformNo, GetManagementTreeListUrl);
        }
        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<TreeNodeInfo> GetOrganizationTreeList(long PlatformNo)
        {
            return GetTreeListByPlatform(PlatformNo, GetOrganizationTreeListUrl);
        }
        /// <summary>
        /// 获取角色树信息
        /// </summary>
        /// <param name="PlatformNo"></param>
        /// <returns></returns>
        public List<TreeNodeInfo> GetRolesTreeList(long PlatformNo)
        {
            return GetTreeListByPlatform(PlatformNo, GetRolesTreeListUrl);
        }
        /// <summary>
        /// 获取树列表【平台】
        /// </summary>
        /// <param name="PlatformNo"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        private List<TreeNodeInfo> GetTreeListByPlatform(long PlatformNo, string Url)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = Url;
                request.SetValue("platformNo", PlatformNo);
                return sdkUtil.PostForResultListVo<TreeNodeInfo>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<TreeNodeInfo>();
        }
    }
}
