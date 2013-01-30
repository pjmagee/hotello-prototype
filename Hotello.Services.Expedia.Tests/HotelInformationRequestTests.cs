using Hotello.Services.Expedia.Hotels.Api;
using NUnit.Framework;

namespace Hotello.Services.Expedia.Tests
{
    public class HotelInformationRequestTests
    {
        private RestExpediaService _expediaService;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _expediaService = new RestExpediaService();
            _expediaService.ApiKey = "ty7wujrv6jc2vbrm2cpnmear";
            _expediaService.Cid = 55505;
            _expediaService.CurrencyCode = "GBP";
            _expediaService.MinorRev = 13;
            _expediaService.Locale = "en_GB";
        }
    }
}
