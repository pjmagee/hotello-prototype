using System.Diagnostics;
using Hotello.Services.Expedia.Hotels.Api;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Hotello.Services.Expedia.Module
{
    public class ExpediaModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<AbstractExpediaService>() // This abstract definition
                .ToProvider<ExpediaServiceProvider>() // To this implementation provider
                .InRequestScope() // in HTTP context Scope
                .OnActivation((context, service) => Debug.WriteLine("Expedia Service Activated"))
                .OnDeactivation((context, service) => Debug.WriteLine("Expedia Service Deactivated")); 
        }
    }
}
