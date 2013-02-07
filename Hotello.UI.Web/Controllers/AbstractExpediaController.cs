using System.Diagnostics;
using System.Reflection;
using Hotello.Services.Expedia.Hotels.Api;
using Hotello.UI.Web.Attributes;
using Hotello.UI.Web.Filters;

namespace Hotello.UI.Web.Controllers
{
    [SessionFilter]
    public abstract class AbstractExpediaController : BootstrapBaseController
    {
        // Our injected service per http scope request
        protected AbstractExpediaService _expediaService;

        /// <summary>
        /// Since there are parts of the expedia request which should always include some values
        /// we ensure any expedia requests use the custom session filter attribute 
        /// to set the ip and user agent when sending the expedia request
        /// </summary>
        public virtual void CommonWebRequestInjector()
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name + " Called by " + this.GetType().Name);

            _expediaService.CustomerSessionId = Session["CustomerSessionId"] as string;
            _expediaService.CustomerIpAddress = HttpContext.Request.UserHostAddress;
            _expediaService.CustomerUserAgent = HttpContext.Request.UserAgent;

        }
    }
}