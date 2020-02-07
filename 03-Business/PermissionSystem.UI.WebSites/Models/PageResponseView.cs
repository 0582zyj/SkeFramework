using MicrosServices.Helper.Core.DTO;
using SkeFramework.Core.Network.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PermissionSystem.UI.WebSites.Models
{
    /// <summary>
    /// Page页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResponseView<T> : PageResponse<T>
    {
        public PageDTO pageDTO { get; set; }

        public PageResponseView(PageResponse<T> pageResponse)
        {
            this.page = pageResponse.page;
            this.dataList = pageResponse.dataList;
            this.pageDTO = new PageDTO(pageResponse.page.PageIndex, pageResponse.page.TotalCount, pageResponse.page.PageSize);
        }

    }
}