using System;
using System.Linq;
using Hotello.Services.Google.DistanceMatrix.Models;
using Ninject;
using RestSharp;
using RestSharp.Validation;

namespace Hotello.Services.Google.DistanceMatrix.Api
{
    public class DistanceMatrixService : IDistanceMatrixService
    {
        private readonly string _apiKey;
        private const string BaseUrl = "http://maps.googleapis.com/maps/api/";
        
        [Inject]
        public DistanceMatrixService(string apiKey)
        {
            if (apiKey == null) // Guard Clause
            {
                // throw new ArgumentNullException("apiKey");
            }

            _apiKey = apiKey;
        }

        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = BaseUrl;

            request.RequestFormat = DataFormat.Json;
            // request.AddParameter("key", _apiKey);

            IRestResponse<T> restResponse = client.Execute<T>(request);
            return restResponse.Data;
        }

        public DistanceMatrixResponse GetDistanceMatrix(DistanceMatrixRequest distanceMatrixRequest)
        {
            Require.Argument("distanceMatrixRequest", distanceMatrixRequest); // Guard Clause
            
            RestRequest request = new RestRequest
            {
                Resource = "distancematrix/json"
            };

            // destinations=Darling+Harbour+NSW+Australia|24+Sussex+Drive+Ottawa+ON|Capitola+CA

            request.AddParameter("destinations", string.Join("|", distanceMatrixRequest.Destinations));

            // origins=Bobcaygeon+ON|41.43206,-81.38992

            request.AddParameter("origins", string.Join("|", distanceMatrixRequest.Origins));
            
            request.AddParameter("sensor", "false");

            // units=imperial returns distances in miles and feet.
            request.AddParameter("units", "imperial");

            DistanceMatrixResponse response = Execute<DistanceMatrixResponse>(request);

            return response;
        }
    }
}