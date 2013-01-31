using System.Web.Mvc;
using Hotello.UI.Web.Controllers;

namespace Hotello.UI.Web.Attributes
{
    public class SessionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var expediaController = filterContext.Controller as AbstractExpediaController;

            if (expediaController != null)
            {
                // All expedia based API requests we must include the IP Address and User Agent of the user
                expediaController.CommonWebRequestInjector();
            }

            base.OnActionExecuting(filterContext);
        }
    }
}