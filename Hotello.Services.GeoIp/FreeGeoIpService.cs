using System;
using RestSharp;
using RestSharp.Validation;

namespace Hotello.Services.GeoIp
{
    public class FreeGeoIpService : IGeoLookupService
    {
        private const string BaseUrl = "http://freegeoip.net/json";

        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BaseUrl).AbsoluteUri;

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            return response.Data;
        }

        public GeoLookUpResponse GetGeoFromIp(GeoLookUpRequest geoLookUpRequest)
        {
            Require.Argument("geoLookUpRequest", geoLookUpRequest);

            RestRequest restRequest = new RestRequest("/?{ip}");
            restRequest.AddParameter("ip", geoLookUpRequest.IpAddress, ParameterType.UrlSegment);
            restRequest.RootElement = "";
            

            return Execute<GeoLookUpResponse>(restRequest);
        }
    }
}