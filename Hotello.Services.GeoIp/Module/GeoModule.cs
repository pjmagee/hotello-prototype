using System.Diagnostics;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Hotello.Services.GeoIp.Module
{
    public class GeoModule : NinjectModule
    {
        public override void Load()
        {
           Bind<IGeoLookupService>()
                .To<FreeGeoIpService>()
                .InRequestScope()
                .OnActivation((context, service) => Debug.WriteLine("Geo Lookup Service Activated"))
                .OnDeactivation((context, service) => Debug.WriteLine("Geo Loopup Service Deactivated"));
        }
    }
}
