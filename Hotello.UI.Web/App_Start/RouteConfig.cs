using System.Web.Mvc;
using System.Web.Routing;

namespace Hotello.UI.Web.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Search", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Search",
               url: "Search/{action}/",
               defaults: new { controller = "Search", action = "Index" });

        }
    }
}