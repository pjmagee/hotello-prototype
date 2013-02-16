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
               name: "Search",
               url: "Search/criteria",
               defaults: new
                   {
                       controller = "Search", 
                       action = "Index"
                   });

            routes.MapRoute(
                name: "Results",
                url: "Search/criteria/results/{page}",
                defaults: new
                    {
                        controller = "Search",
                        action = "Results",
                        page = UrlParameter.Optional,
                    });

            routes.MapRoute(
                name: "Hotels",
                url: "Hotels/Details/{id}/{country}/{city}/{name}",
                defaults: new
                {
                    controller = "Hotels",
                    action = "Information",
                    id = UrlParameter.Optional,
                    country = UrlParameter.Optional,
                    city = UrlParameter.Optional,
                    name = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Rooms",
                url: "Rooms/Availability/{id}/{country}/{city}/{name}",
                defaults: new
                {
                    controller = "Rooms",
                    action = "Availability",
                    id = UrlParameter.Optional,
                    country = UrlParameter.Optional,
                    city = UrlParameter.Optional,
                    name = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Distance",
                url: "Distance/Index/{page}",
                defaults: new
                {
                    controller = "Distance",
                    action = "Index",
                    page = UrlParameter.Optional
                });
           

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                    {
                        controller = "Search", 
                        action = "Index", 
                        id = UrlParameter.Optional
                    }
            );

            

        }
    }
}