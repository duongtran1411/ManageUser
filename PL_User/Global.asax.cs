using BLL_User.Extension;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PL_User
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            StaticData.GetPermissionType();
        }
    }
}
