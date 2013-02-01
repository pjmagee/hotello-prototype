using System;
using System.Diagnostics;
using System.Linq;
using Hotello.Services.Expedia.Hotels.Models.Request;
using Hotello.Services.Expedia.Hotels.Models.Response;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Extensions;
using RestSharp.Validation;

namespace Hotello.Services.Expedia.Hotels.Api
{
    // HTTP request headers should include Accept-Encoding: gzip,deflate
    // http://developer.ean.com/general_info/Properly_Handling_Data_Transfers
    // User-agent should NOT include MSIE anywhere in the string, which will cause the results not to be compressed.
    // Ensuring that compressed content is transmitted will greatly speed up the response time of your results, especially when transferring large messages.

    
    public class RestExpediaService : AbstractExpediaService
    {
        private const string BaseUrl = "http://api.ean.com/ean-services/rs/hotel/v3";

        // Injected properties
        public override long Cid { get; set; }
        public override string ApiKey { get; set; }
        public override int MinorRev { get; set; }
        public override string Locale { get; set; }
        public override string CurrencyCode { get; set; }
        public override string CustomerSessionId { get; set; }
        public override string CustomerIpAddress { get; set; }
        public override string CustomerUserAgent { get; set; } 
        public override string Sig { get; set; }

        private T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BaseUrl).AbsoluteUri;
            client.ClearHandlers();
            client.AddHandler("application/json",  new JsonDeserializer());
            client.AddDefaultParameter("Accept-Encoding", "gzip,deflate", ParameterType.HttpHeader);
            
            request.AddParameter("apiKey", ApiKey);
            request.AddParameter("cid", Cid); 
            request.AddParameter("minorRev", MinorRev);
            request.AddParameter("currencyCode", CurrencyCode);
            request.AddParameter("locale", Locale);

            if(CustomerUserAgent.HasValue())
                request.AddParameter("customerUserAgent", CustomerUserAgent);

            if(CustomerSessionId.HasValue() && CustomerSessionId != null)       
                request.AddParameter("customerSessionId", CustomerSessionId);

            if(CustomerIpAddress.HasValue())
                request.AddParameter("customerIpAddress", CustomerIpAddress);

            request.Parameters.ForEach(p => Debug.WriteLine(p.Name + "=" + p.Value));

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            return response.Data;
        }

        public override HotelListResponse GetHotelAvailabilityList(HotelListRequest hotelListRequest)
        {
            Require.Argument("hotelListRequest", hotelListRequest);

            RestRequest restRequest = new RestRequest();
            restRequest.Resource = "list";
            restRequest.Method = Method.GET;
            restRequest.RootElement = "HotelListResponse";
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.DateFormat = "MMddyyyy";

            if (hotelListRequest.ArrivalDate.HasValue)
                restRequest.AddParameter("arrivalDate", hotelListRequest.ArrivalDate.Value.ToShortDateString(), ParameterType.GetOrPost);

            if (hotelListRequest.DepartureDate.HasValue)
                restRequest.AddParameter("departureDate", hotelListRequest.DepartureDate.Value.ToShortDateString(), ParameterType.GetOrPost);

            if (hotelListRequest.CacheKey.HasValue() && hotelListRequest.CacheLocation.HasValue())
            {
                // This is a paging request

                restRequest.AddParameter("cacheKey", hotelListRequest.CacheKey);
                restRequest.AddParameter("cacheLocation", hotelListRequest.CacheLocation);

                return Execute<HotelListResponse>(restRequest);
            }

            // If I gave you a destination id, use that
            if (hotelListRequest.DestinationId.HasValue()) 
            {
                restRequest.AddParameter("destinationId", hotelListRequest.DestinationId); 
            }
            else if (hotelListRequest.DestinationString.HasValue())
            {
                // otherwise, use the destination string I gave
                restRequest.AddParameter("destinationString", hotelListRequest.DestinationString);
            }
            else
            {
                // if destination id nor destination string have values, try and use city, state, county params

                if (hotelListRequest.City.HasValue())
                    restRequest.AddParameter("city", hotelListRequest.City);

                if (hotelListRequest.CountryCode.HasValue())
                    restRequest.AddParameter("countryCode", hotelListRequest.CountryCode);

                if (hotelListRequest.StateProvinceCode.HasValue())
                    restRequest.AddParameter("stateProvinceCode", hotelListRequest.StateProvinceCode);
            }

            // Default this to 200 per request
            restRequest.AddParameter("numberOfResults", 200);

            restRequest.AddParameter("supplierType", "E"); // Expedia Collect  

            HandleFilteringMethods(restRequest, hotelListRequest);

            // REST Room and guest counts are formatted differently than XML and SOAP. 
            // Review the REST room format section on the hotel list restRequest page.
            // http://developer.ean.com/docs/read/hotels/version_3/request_hotel_rooms

            // &room[room number, starting with 1]=
            // [number of adults],
            // [comma-delimited list of children's ages] 

            if (hotelListRequest.RoomGroup != null)
            {
                for(int i = 0; i < hotelListRequest.NumberOfBedRooms; i++)
                {
                    var parameter = new Parameter();
                    parameter.Value = hotelListRequest.RoomGroup[i].NumberOfAdults + (hotelListRequest.RoomGroup[i].ChildAges == null ? "" : ("," + String.Join(",", hotelListRequest.RoomGroup[i].ChildAges.Take(hotelListRequest.RoomGroup[i].NumberOfChildren).ToArray())));
                    parameter.Type = ParameterType.GetOrPost;
                    parameter.Name = "room" + (hotelListRequest.RoomGroup.IndexOf(hotelListRequest.RoomGroup[i]) + 1);
                    restRequest.AddParameter(parameter);
                }
            }

            // for performance boost
            restRequest.AddParameter("supplierCacheTolerance", "MAX_ENHANCED");

            return Execute<HotelListResponse>(restRequest);
        }

        public override HotelListResponse GetHotelActiveList(HotelListRequest hotelListRequest)
        {
            Require.Argument("hotelListRequest", hotelListRequest);

            RestRequest restRequest = new RestRequest();
            restRequest.Resource = "list";
            restRequest.Method = Method.GET;
            restRequest.RootElement = "HotelListResponse";

            if (hotelListRequest.DestinationString.HasValue())
            {
                // Free text destination search
                restRequest.AddParameter("destinationString", hotelListRequest.DestinationString);
            }
            else
            {
                // City, State, Country Search

                if (hotelListRequest.City.HasValue())
                    restRequest.AddParameter("city", hotelListRequest.City);

                if (hotelListRequest.CountryCode.HasValue())
                    restRequest.AddParameter("countryCode", hotelListRequest.CountryCode); 

                if (hotelListRequest.StateProvinceCode.HasValue())
                    restRequest.AddParameter("stateProvinceCode", hotelListRequest.StateProvinceCode); 
            }

            HandleFilteringMethods(restRequest, hotelListRequest);

            return Execute<HotelListResponse>(restRequest);
        }

        public override HotelInformationResponse GetHotelInformation(HotelInformationRequest hotelInformationRequest)
        {
            Require.Argument("hotelInformationRequest", hotelInformationRequest);

            var restRequest = new RestRequest();
            restRequest.Resource = "info";
            restRequest.Method = Method.GET;
            restRequest.RootElement = "HotelInformationResponse";
            restRequest.AddParameter("hotelId", hotelInformationRequest.HotelId);
            restRequest.AddParameter("options", hotelInformationRequest.Options != null ? String.Join(",", hotelInformationRequest.Options) : "DEFAULT");

            return Execute<HotelInformationResponse>(restRequest);
        }



        /**
         * http://api.ean.com/ean‑services/rs/hotel/v3/avail?minorRev=[current minorRev #]
         * &cid=55505
         * &apiKey=[xxx-yourOwnKey-xxx]
         * &customerUserAgent=[xxx]
         * &customerIpAddress=[xxx]
         * &customerSessionId=[xxx]
         * &locale=en_US
         * &currencyCode=USD
         * &hotelId=109496
         * &arrivalDate=08/03/2012
         * &departureDate=08/04/2012
         * &includeDetails=true
         * &room1=2
         * &options=HOTEL_DETAILS 
         */

        public override HotelRoomAvailabilityResponse GetHotelRoomAvailability(HotelRoomAvailabilityRequest roomAvailabilityRequest)
        {
            Require.Argument("roomAvailabilityRequest", roomAvailabilityRequest);

            var request = new RestRequest();
            request.Resource = "avail";
            request.Method = Method.GET;
            request.RequestFormat = DataFormat.Json;
            request.DateFormat = "MMddyyyy";
            request.RootElement = "HotelRoomAvailabilityResponse";
            request.AddParameter("options", "ROOM_TYPES,ROOM_AMENITIES");
            request.AddParameter("hotelId", roomAvailabilityRequest.HotelId);
            request.AddParameter("arrivalDate", roomAvailabilityRequest.ArrivalDate.ToShortDateString());
            request.AddParameter("departureDate", roomAvailabilityRequest.DepartureDate.ToShortDateString());
            request.AddParameter("includeRoomImages", true); 

            if (roomAvailabilityRequest.RoomGroup != null)
            {
                foreach (var room in roomAvailabilityRequest.RoomGroup)
                {
                    var parameter = new Parameter();
                    parameter.Name = "room" + (roomAvailabilityRequest.RoomGroup.IndexOf(room) + 1);
                    parameter.Type = ParameterType.GetOrPost;
                    parameter.Value = room.NumberOfAdults + (room.ChildAges == null || room.ChildAges.Count < 1 ? "" : "," + String.Join(",", room.ChildAges.ToArray()));
                    request.AddParameter(parameter);
                }
            }

            return Execute<HotelRoomAvailabilityResponse>(request);
        }

        public override LocationInfoResponse GetGeoSearch(LocationInfoRequest locationInfoRequest)
        {
            Require.Argument("locationInfoRequest", locationInfoRequest);
            
            RestRequest restRequest = new RestRequest();
            restRequest.Resource = "geoSearch";
            restRequest.Method = Method.GET;
            restRequest.RootElement = "LocationInfoResponse";

            // LOCATION REQUESTS

            if (locationInfoRequest.DestinationString.HasValue())
            {
                // Get Landmarks by Destination String

                restRequest.AddParameter("destinationString", locationInfoRequest.DestinationString);
                restRequest.AddParameter("type", 2); // Landmarks I presume

            }

            return Execute<LocationInfoResponse>(restRequest);
        }

        public override HotelRoomCancellationResponse GetHotelRoomCancel(HotelRoomCancellationRequest hotelRoomCancellationRequest)
        {
            throw new NotImplementedException();
        }

        public override PingResponse GetPing(PingRequest pingRequest)
        {
            throw new NotImplementedException();
        }

        private void HandleFilteringMethods(RestRequest restRequest, HotelListRequest hotelListRequest)
        {

            if (hotelListRequest.PropertyCategories != null)
            {
                restRequest.AddParameter("propertyCategory", string.Join(",", hotelListRequest.PropertyCategories.Select(pc => Convert.ToInt32(pc))));
            }

            if (hotelListRequest.Amenities != null)
            {
                restRequest.AddParameter("amenities", string.Join(",", hotelListRequest.Amenities.Select(a => Convert.ToInt32(a))));
            }

            if (hotelListRequest.MinStarRating.HasValue)
            {
                restRequest.AddParameter("minStarRating", hotelListRequest.MinStarRating.Value);
            } 

            if (hotelListRequest.MaxStarRating.HasValue)
            {
                restRequest.AddParameter("maxStarRating", hotelListRequest.MaxStarRating.Value);
            }
                

        }
    }
}
