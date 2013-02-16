using Hotello.Services.Expedia.Hotels.Api;
using Hotello.Services.Expedia.Hotels.Api.Impl;
using Ninject.Activation;

namespace Hotello.Services.Expedia.Module
{
    /// <summary>
    /// This provider states that a RestExpediaService (implementation)
    /// should be injected into the AbtractExpediaService.
    /// 
    /// Note that with any implementation of the AbstractExpediaService,
    /// it could be injected here and any consumers of the AbstractExpediaService
    /// would be implementation agnostic.
    /// 
    /// </summary>
    public class ExpediaServiceProvider : Provider<AbstractExpediaService>
    {
        protected override AbstractExpediaService CreateInstance(IContext context)
        {
            // TODO: Read API Key and Cid from a Configuration File
            return new RestExpediaService
                {
                    ApiKey = "ty7wujrv6jc2vbrm2cpnmear",
                    Cid = 55505,
                    CurrencyCode = "GBP",
                    MinorRev = 20,
                    Locale = "en_GB"
                };
        }
    }
}