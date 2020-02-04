using PermissionSystem.UI.WebSites.Controllers.Filters;
using System.Web;
using System.Web.Mvc;

namespace PermissionSystem.UI.WebSites
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new PermissionAuthAttribute(0,1));
        }
    }
}
