using System.Web.Mvc;
using System.Web.Routing;

namespace XplicitApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routeCollection)
        {
            routeCollection.Ignore("{resource}.axd/{*pathInfo}");

            routeCollection.MapMvcAttributeRoutes();

            routeCollection.MapRoute(

                name:"Default",
                url:"{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}