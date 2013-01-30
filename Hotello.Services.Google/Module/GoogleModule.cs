using System.Diagnostics;
using Hotello.Services.Google.Places.Api;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Hotello.Services.Google.Module
{
    public class GoogleModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IPlacesService>().To<PlacesService>()
                .InRequestScope() // Per HTTP Request, inject this implementation
                .WithConstructorArgument("apiKey", "AIzaSyCOJC3imDN9AEG0WE_xjNJiAABzpffqpuE") // With this API Key
                .OnActivation((context, service) => Debug.WriteLine("Places Service Activated"))
                .OnDeactivation((context, service) => Debug.WriteLine("Places Service Deactivated"));
        }
    }
}
