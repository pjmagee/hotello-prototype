using System;
using System.Net.Http;
using NUnit.Framework;

namespace Hotello.Services.GeoIp.Tests
{
    [TestFixture]
    public class GeoIpTests
    {


        [Test(Description = "Should return geo data from my IP Address")]
        public void FreeGeoFromIpTest()
        {
            HttpClient client = new HttpClient();
            string ip = client.GetStringAsync("http://api.exip.org/?call=ip").Result;
                              
            FreeGeoIpService service = new FreeGeoIpService();
            GeoLookUpResponse geoLookUpResponse = service.GetGeoFromIp(new GeoLookUpRequest() {IpAddress = ip});


            Console.WriteLine(geoLookUpResponse.City);
            Console.WriteLine(geoLookUpResponse.CountryCode);
        }
    }
}
