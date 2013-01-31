using System.Web;
using System.Web.Mvc;

namespace Hotello.UI.Web.Twitter.Bootstrap.MVC4.Sample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}