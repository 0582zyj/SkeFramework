﻿using MicrosServices.Entities.Common.PublishDeploy;
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
        private static readonly string GetListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Project/GetList";
        private static readonly string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Project/GetPageList";
        private static readonly string GetInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Project/GetInfo";
        private static readonly string AddUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Project/Create";
        private static readonly string DeleteUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Project/Delete";
        private static readonly string UpdateUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Project/Update";
        private static readonly string GetOptionValueUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Project/GetOptionValues";
        private static readonly string PublishDeployUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/Project/Publish";
        

        /// <summary>
        /// 获取所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<PdProject> GetList()
        {
            List<PdProject> menus = new List<PdProject>();
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetListUrl
                };
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    return JsonConvert.DeserializeObject<List<PdProject>>(JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return menus;
        }
        /// <summary>
        /// 获分页列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public PageResponse<PdProject> GetPageList(PageModel page, string keywords = "")
        {
            PageResponse<PdProject> menus = new PageResponse<PdProject>();
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("PageIndex", page.PageIndex);
                request.SetValue("PageSize", page.PageSize);
                request.SetValue("keywords", keywords);
                request.Url = GetPageUrl;
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    Method = RequestTypeEnums.GET
                });
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    menus = JsonConvert.DeserializeObject<PageResponse<PdProject>>(JsonConvert.SerializeObject(data));
                    return menus;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return menus;
        }
        /// <summary>
        /// 根据主键ID获取信息
        /// </summary>
        /// <returns></returns>
        public JsonResponses GetInfo(int id)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("id", id.ToString());
                request.Url = GetInfoUrl;
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.GetReqUrl(),
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.GET
                });
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    responses.data = JsonConvert.DeserializeObject<PdProject>(JsonConvert.SerializeObject(data));
                }
                return responses;
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
                RequestBase request = new RequestBase();
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
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.POST_FORM
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
        /// 新增
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses Update(PdProject model)
        {
            try
            {
                RequestBase request = new RequestBase();
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
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.POST_FORM
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
        /// 删除
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses Delete(int id)
        {
            try
            {
                RequestBase request = new RequestBase();
                request.SetValue("id", id);
                request.Url = DeleteUrl;
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.POST_FORM
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
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<OptionValue> GetOptionValues()
        {
            try
            {
                RequestBase request = new RequestBase
                {
                    Url = GetOptionValueUrl
                };
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    Method = RequestTypeEnums.GET
                });
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    return JsonConvert.DeserializeObject<List<OptionValue>>(JsonConvert.SerializeObject(data));
                }
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
                RequestBase request = new RequestBase();
                request.SetValue("id", id);
                request.Url = PublishDeployUrl;
                string result = HttpHelper.Example.GetWebData(new BrowserPara()
                {
                    Uri = request.Url,
                    PostData = request.GetRequestData(),
                    Method = RequestTypeEnums.POST_FORM
                });
                return JsonConvert.DeserializeObject<JsonResponses>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonResponses.Failed;
        }
    }
}
