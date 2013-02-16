using System.Diagnostics;
using Hotello.Services.Google.DistanceMatrix.Api;
using Hotello.Services.Google.Places.Api;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Hotello.Services.Google.Module
{
    public class GoogleModule : NinjectModule
    {
        public override void Load()
        {
            const string apiKey = "AIzaSyCOJC3imDN9AEG0WE_xjNJiAABzpffqpuE";

            Bind<IPlacesService>().To<PlacesService>()
                .InRequestScope() // Per HTTP Request, inject this implementation
                .WithConstructorArgument("apiKey", apiKey) // With this API Key
                .OnActivation((context, service) => Debug.WriteLine("Places Service Activated"))
                .OnDeactivation((context, service) => Debug.WriteLine("Places Service Deactivated"));


            Bind<IDistanceMatrixService>().To<DistanceMatrixService>()
                .InRequestScope() // Per HTTP Request, inject a new instance of this implementation
                .WithConstructorArgument("apiKey", apiKey) // With this API Key
                .OnActivation((context, service) => Debug.WriteLine("Matrix Service Activated"))
                .OnDeactivation((context, service) => Debug.WriteLine("Matrix Service Deactivated"));

            
        }
    }
}
