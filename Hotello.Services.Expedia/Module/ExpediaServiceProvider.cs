using Hotello.Services.Expedia.Hotels.Api;
using Ninject.Activation;

namespace Hotello.Services.Expedia.Module
{
    public class ExpediaServiceProvider : Provider<AbstractExpediaService>
    {
        protected override AbstractExpediaService CreateInstance(IContext context)
        {
            // always inject with these variables

            RestExpediaService service = new RestExpediaService
                {
                    ApiKey = "ty7wujrv6jc2vbrm2cpnmear",
                    Cid = 55505,
                    CurrencyCode = "GBP",
                    MinorRev = 20,
                    Locale = "en_GB"
                };

            return service;
        }
    }
}