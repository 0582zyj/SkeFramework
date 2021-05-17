using MicrosServices.Entities.Common.BaseSystem;
using MicrosServices.Helper.Core.Extends;
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

namespace MicrosServices.SDK.AdminSystem
{
   public class DictionaryTypeSDK
    {
        private static readonly string GetListUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/dictionarytype/getList";
        private static readonly string GetPageUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/dictionarytype/getPageList";
        private static readonly string GetInfoUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/dictionarytype/getInfo";
        private static readonly string AddUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/dictionarytype/create";
        private static readonly string DeleteUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/dictionarytype/delete";
        private static readonly string UpdateUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/dictionarytype/update";
        private static readonly string GetOptionValueUrl = NetwordConstants.Instance().GetBaseUrl() + "/api/dictionarytype/getOptionValues";

        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<BsDictionaryType> GetList()
        {
            List<BsDictionaryType> menus = new List<BsDictionaryType>();
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetListUrl;
                string result = HttpHelper.Example.GetWebData(request);
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    return JsonConvert.DeserializeObject<List<BsDictionaryType>>(JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return menus;
        }
        /// <summary>
        /// 获取菜单所有列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public PageResponse<BsDictionaryType> GetPageList(PageModel page, string keywords = "", long DictionaryNo = -1)
        {
            PageResponse<BsDictionaryType> menus = new PageResponse<BsDictionaryType>();
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("pageindex", page.PageIndex);
                request.SetValue("pagesize", page.PageSize);
                request.SetValue("keywords", keywords);
                request.SetValue("queryNo", DictionaryNo);
                request.Url = GetPageUrl;
                string result = HttpHelper.Example.GetWebData(request);
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    menus = JsonConvert.DeserializeObject<PageResponse<BsDictionaryType>>(JsonConvert.SerializeObject(data));
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
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.SetValue("id", id.ToString());
                request.Url = GetInfoUrl;
                string result = HttpHelper.Example.GetWebData(request);
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    responses.data = JsonConvert.DeserializeObject<BsDictionaryType>(JsonConvert.SerializeObject(data));
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
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResponses DictionaryTypeAdd(BsDictionaryType model)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("dicType", model.DicType);
                request.SetValue("descriptions", model.Descriptions);
                request.SetValue("platformNo", model.PlatformNo);
                request.SetValue("enabled", model.Enabled);
                request.SetValue("inputUser", model.InputUser);
                request.SetValue("inputTime", model.InputTime);
                request.SetValue("updateUser", model.UpdateUser);
                request.SetValue("updateTime", model.UpdateTime);
                request.Url = AddUrl;
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
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResponses DictionaryTypeUpdate(BsDictionaryType model)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("dicType", model.DicType);
                request.SetValue("descriptions", model.Descriptions);
                request.SetValue("platformno", model.PlatformNo);
                request.SetValue("enabled", model.Enabled);
                request.SetValue("id", model.id);
                request.Url = UpdateUrl;
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
        /// 删除
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResponses DictionaryDelete(int id)
        {
            try
            {
                RequestBase request = RequestBase.PostForm.Clone() as RequestBase;
                request.SetValue("id", id);
                request.Url = DeleteUrl;
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
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<DictionaryOptionValue> GetDictionaryOptionValues(long PlatformNo)
        {
            try
            {
                RequestBase request = RequestBase.Get.Clone() as RequestBase;
                request.Url = GetOptionValueUrl;
                request.SetValue("platformNo", PlatformNo);
                string result = HttpHelper.Example.GetWebData(request);
                JsonResponses responses = JsonConvert.DeserializeObject<JsonResponses>(result);
                if (responses.code == JsonResponses.SuccessCode)
                {
                    object data = responses.data;
                    return JsonConvert.DeserializeObject<List<DictionaryOptionValue>>(JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new List<DictionaryOptionValue>();
        }

    }
}
