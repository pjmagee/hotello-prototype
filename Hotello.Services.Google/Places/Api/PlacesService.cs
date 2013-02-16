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
        
        [Inject]
        public PlacesService(string apiKey)
        {
            if (apiKey == null) // Guard Clause
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

        /// <summary>
        /// <see cref="https://developers.google.com/maps/documentation/places/#PlaceSearches"/>
        /// https://maps.googleapis.com/maps/api/place/search/output?parameters
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        public virtual SearchResponse Search(SearchRequest searchRequest)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// https://developers.google.com/maps/documentation/places/#PlaceDetails
        /// https://maps.googleapis.com/maps/api/place/details/output?parameters
        /// </summary>
        /// <param name="detailsRequest"></param>
        /// <returns></returns>
        public virtual DetailsResponse Details(DetailsRequest detailsRequest)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// https://developers.google.com/maps/documentation/places/#PlaceCheckins
        /// https://maps.googleapis.com/maps/api/place/check-in/output?parameters
        /// </summary>
        /// <param name="checkInRequest"></param>
        /// <returns></returns>
        public virtual CheckInResponse CheckIn(CheckInRequest checkInRequest)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// https://developers.google.com/maps/documentation/places/#PlaceReports
        /// https://maps.googleapis.com/maps/api/place/add/output?parameters
        /// https://maps.googleapis.com/maps/api/place/delete/output?parameters
        /// </summary>
        /// <param name="reportRequest"></param>
        /// <returns></returns>
        public virtual ReportResponse Report(ReportRequest reportRequest)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// https://developers.google.com/maps/documentation/places/autocomplete
        /// https://maps.googleapis.com/maps/api/place/autocomplete/output?parameters
        /// </summary>
        /// <param name="autocompletionRequest"></param>
        /// <returns></returns>
        public virtual AutocompletionResponse Autocomplete(AutocompletionRequest autocompletionRequest)
        {
            RestRequest request = new RestRequest
                {
                    Resource = "autocomplete/json"
                };

            request.AddParameter("input", autocompletionRequest.Input);
            request.AddParameter("sensor", "false");

            if (autocompletionRequest.Types.HasValue())
                request.AddParameter("types", autocompletionRequest.Types);

            AutocompletionResponse response = Execute<AutocompletionResponse>(request);

            return response;
        }
    }
}