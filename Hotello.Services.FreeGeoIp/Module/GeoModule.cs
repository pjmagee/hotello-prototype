using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Hotello.Services.GeoIp.Module
{
    public class GeoModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IGeoLookupService>()
                .To<FreeGeoIpService>()
                .InRequestScope()
                .OnActivation((context, service) => Debug.WriteLine("Geo Lookup Service Activated"))
                .OnDeactivation((context, service) => Debug.WriteLine("Geo Loopup Service Deactivated"));
        }
    }
}
