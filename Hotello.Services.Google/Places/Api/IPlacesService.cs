using Hotello.Services.Google.Places.Models.Autocomplete;
using Hotello.Services.Google.Places.Models.CheckIn;
using Hotello.Services.Google.Places.Models.Details;
using Hotello.Services.Google.Places.Models.Report;
using Hotello.Services.Google.Places.Models.Search;

namespace Hotello.Services.Google.Places.Api
{
    /// <summary>
    /// This interface defines a Contract for any Google Api Implementation 
    /// </summary>
    public interface IPlacesService
    {
        /// <summary>
        /// <see cref="https://developers.google.com/maps/documentation/places/#PlaceSearches"/>
        /// https://maps.googleapis.com/maps/api/place/search/output?parameters
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        SearchResponse Search(SearchRequest searchRequest);

        /// <summary>
        /// https://developers.google.com/maps/documentation/places/#PlaceDetails
        /// https://maps.googleapis.com/maps/api/place/details/output?parameters
        /// </summary>
        /// <param name="detailsRequest"></param>
        /// <returns></returns>
        DetailsResponse Details(DetailsRequest detailsRequest);

        /// <summary>
        /// https://developers.google.com/maps/documentation/places/#PlaceCheckins
        /// https://maps.googleapis.com/maps/api/place/check-in/output?parameters
        /// </summary>
        /// <param name="checkInRequest"></param>
        /// <returns></returns>
        CheckInResponse CheckIn(CheckInRequest checkInRequest);

        /// <summary>
        /// https://developers.google.com/maps/documentation/places/#PlaceReports
        /// https://maps.googleapis.com/maps/api/place/add/output?parameters
        /// https://maps.googleapis.com/maps/api/place/delete/output?parameters
        /// </summary>
        /// <param name="reportRequest"></param>
        /// <returns></returns>
        ReportResponse Report(ReportRequest reportRequest);

        /// <summary>
        /// https://developers.google.com/maps/documentation/places/autocomplete
        /// https://maps.googleapis.com/maps/api/place/autocomplete/output?parameters
        /// </summary>
        /// <param name="autocompletionRequest"></param>
        /// <returns></returns>
        AutocompletionResponse Autocomplete(AutocompletionRequest autocompletionRequest);
    }
}