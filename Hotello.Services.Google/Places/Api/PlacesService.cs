using System;
using Hotello.Services.Google.Places.Models.Autocomplete;
using Hotello.Services.Google.Places.Models.CheckIn;
using Hotello.Services.Google.Places.Models.Details;
using Hotello.Services.Google.Places.Models.Report;
using Hotello.Services.Google.Places.Models.Search;
using Ninject;
using RestSharp;
using RestSharp.Extensions;

namespace Hotello.Services.Google.Places.Api
{
    public class PlacesService : IPlacesService
    {
        private readonly string _apiKey;
        private const string BaseUrl = "https://maps.googleapis.com/maps/api/place/";


        public PlacesService(string apiKey)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException("apiKey");
            }

            _apiKey = apiKey;
        }

        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = BaseUrl;

            request.RequestFormat = DataFormat.Json;
            request.AddParameter("key", _apiKey);

            IRestResponse<T> restResponse = client.Execute<T>(request);
            return restResponse.Data;
        }

        public virtual SearchResponse Search(SearchRequest searchRequest)
        {
            throw new System.NotImplementedException();
        }

        public virtual DetailsResponse Details(DetailsRequest detailsRequest)
        {
            throw new System.NotImplementedException();
        }

        public virtual CheckInResponse CheckIn(CheckInRequest checkInRequest)
        {
            throw new System.NotImplementedException();
        }

        public virtual ReportResponse Report(ReportRequest reportRequest)
        {
            throw new System.NotImplementedException();
        }

        public virtual AutocompletionResponse Autocomplete(AutocompletionRequest autocompletionRequest)
        {
            RestRequest request = new RestRequest();
            request.Resource = "autocomplete/json";
            
            
            request.AddParameter("input", autocompletionRequest.Input);
            request.AddParameter("sensor", "false");

            if (autocompletionRequest.Types.HasValue())
                request.AddParameter("types", autocompletionRequest.Types);

            AutocompletionResponse autocompletionResponse = Execute<AutocompletionResponse>(request);

            return autocompletionResponse;
        }
    }
}