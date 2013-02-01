using Hotello.Services.Expedia.Hotels.Api;
using NUnit.Framework;

namespace Hotello.Services.Expedia.Tests
{
    public class HotelInformationTests
    {
        private RestExpediaService expediaService;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            expediaService = new RestExpediaService();
            expediaService.ApiKey = "ty7wujrv6jc2vbrm2cpnmear";
            expediaService.Cid = 55505;
            expediaService.CurrencyCode = "GBP";
            expediaService.MinorRev = 13;
            expediaService.Locale = "en_GB";
        }
    }
}
