using MicrosServices.Entities.Common.PublishDeploy;
using MicrosServices.Helper.Core.Common;
using MicrosServices.SDK.PublishDeploy.DataUtil;
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

namespace MicrosServices.SDK.PublishDeploy
{
   public class ProjectSdk
    {
        private static readonly string GetListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/project/getList";
        private static readonly string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/project/getPageList";
        private static readonly string GetInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/project/getInfo";
        private static readonly string AddUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/project/create";
        private static readonly string DeleteUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/project/delete";
        private static readonly string UpdateUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/project/update";
        private static readonly string GetOptionValueUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/project/getOptionValues";
        private static readonly string PublishDeployUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/project/publish";

        private SdkUtil sdkUtil = new SdkUtil();

        /// <summary>
        /// 获取所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<PdProject> GetList()
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetListUrl;
                return sdkUtil.PostForResultListVo<PdProject>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<PdProject>();
        }
        /// <summary>
        /// 获分页列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public PageResponse<PdProject> GetPageList(PageModel page, string keywords = "")
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetPageUrl;
                request.SetValue("pageIndex", page.PageIndex);
                request.SetValue("pageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                return sdkUtil.PostForResultVo<PageResponse<PdProject>>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new PageResponse<PdProject>();
        }
        /// <summary>
        /// 根据主键ID获取信息
        /// </summary>
        /// <returns></returns>
        public JsonResponses GetInfo(int id)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("id", id.ToString());
                request.Url = GetInfoUrl;
                return sdkUtil.PostForVo(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses Add(PdProject model)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("ProjectNo", model.ProjectNo);
                request.SetValue("Name", model.Name);
                request.SetValue("VersionType", model.VersionType);
                request.SetValue("VersionUrl", model.VersionUrl);
                request.SetValue("GitBranch", model.GitBranch);
                request.SetValue("GitBinPath", model.GitBinPath);
                request.SetValue("SourcePath", model.SourcePath);
                request.SetValue("MSBuildPath", model.MSBuildPath);
                request.SetValue("ProjectFile", model.ProjectFile);
                request.SetValue("notifyEmails", model.notifyEmails);
                request.SetValue("InputUser", model.InputUser);
                request.SetValue("InputTime", model.InputTime);
                request.Url = AddUrl;
                return sdkUtil.PostForVo(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses Update(PdProject model)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("id", model.id);
                request.SetValue("ProjectNo", model.ProjectNo);
                request.SetValue("Name", model.Name);
                request.SetValue("VersionType", model.VersionType);
                request.SetValue("VersionUrl", model.VersionUrl);
                request.SetValue("GitBranch", model.GitBranch);
                request.SetValue("GitBinPath", model.GitBinPath);
                request.SetValue("SourcePath", model.SourcePath);
                request.SetValue("MSBuildPath", model.MSBuildPath);
                request.SetValue("ProjectFile", model.ProjectFile);
                request.SetValue("notifyEmails", model.notifyEmails);
                request.SetValue("InputUser", model.InputUser);
                request.Url = UpdateUrl;
                return sdkUtil.PostForVo(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses Delete(int id)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("id", id);
                request.Url = DeleteUrl;
                return sdkUtil.PostForVo(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<OptionValue> GetOptionValues()
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetOptionValueUrl;
                return sdkUtil.PostForResultListVo<OptionValue>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<OptionValue>();
        }
        /// <summary>
        /// 立即发布
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses PublishDeploy(int id)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("id", id);
                request.Url = PublishDeployUrl;
                return sdkUtil.PostForVo(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
    }
}
