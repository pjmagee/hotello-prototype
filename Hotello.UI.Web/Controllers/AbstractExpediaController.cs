﻿using System.Diagnostics;
using System.Reflection;
using System.Web.Mvc;
using Hotello.Services.Expedia.Hotels.Api;
using Hotello.UI.Web.Filters;

namespace Hotello.UI.Web.Controllers
{
    [SessionFilter]
    public abstract class AbstractExpediaController : Controller
    {
        protected AbstractExpediaService _expediaService;

        /// <summary>
        /// Since there are parts of the expedia request which should always include some values
        /// we ensure any expedia requests use the custom session filter attribute 
        /// to set the ip and user agent when sending the expedia request
        /// </summary>
        public virtual void CommonWebRequestInjector()
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name + " Called by " + this.GetType().Name);

            // From our last expedia response
            _expediaService.CustomerSessionId = ViewBag.CustomerSessionId;

            // Http context info for expedia request
            _expediaService.CustomerIpAddress = HttpContext.Request.UserHostAddress;
            _expediaService.CustomerUserAgent = HttpContext.Request.UserAgent;
        }
    }
}