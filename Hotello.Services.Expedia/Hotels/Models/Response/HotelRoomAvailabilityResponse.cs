using System;
using System.Collections.Generic;

namespace Hotello.Services.Expedia.Hotels.Models.Response
{
    public class HotelRoomAvailabilityResponse
    {
        public EanWsError EanWsError { get; set; }
        public string CustomerSessionId { get; set; }

        /// <summary>
        /// ID for the property. This same ID will be used in any subsequent reservation requests.
        /// </summary>
        public long HotelId { get; set; }

        /// <summary>
        /// Confirms the check-in date submitted in the request.
        /// </summary>
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Confirms the check-out date submitted in the request.
        /// </summary>
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Name of the hotel
        /// </summary>
        public string HotelName { get; set; }

        /// <summary>
        /// Hotel street address
        /// </summary>
        public string HotelAddress { get; set; }

        /// <summary>
        /// Two character code for the state/province containing the specified city.
        /// Returns only for US, CA, and AU country codes.
        /// </summary>
        public string HotelStateProvince { get; set; }

        /// <summary>
        /// Hotel city
        /// </summary>
        public string HotelCity { get; set; }

        /// <summary>
        /// Hotel Country
        /// </summary>
        public string HotelCountry { get; set; }

        /// <summary>
        /// Confirms the number of Room nodes sent in the request.
        /// </summary>
        public int NumberOfRoomsRequested { get; set; }

        /// <summary>
        /// Must be displayed if returned by the property. 
        /// May include fees that can be incurred at check-in, instructions for after-hours check-in, etc
        /// </summary>
        public string CheckInInstructions { get; set; }


        /// <summary>
        /// <para>
        /// Key to the search parameters and other values determining the rate.
        /// Note that this value is almost always different from the value returned in the hotel availability response - always pass over the value from this response to the booking request.
        /// </para> 
        /// </summary>
        public string RateKey { get; set; }

        /// <summary>
        /// The Number of Trip Advisor Reviews
        /// </summary>
        public int TripAdvisorReviewCount { get; set; }

        /// <summary>
        /// The image of the Trip avistor Rating
        /// </summary>
        public string TripAdvisorRatingUrl { get; set; }


        /// <summary>
        /// Trip Advisor Rating
        /// </summary>
        public decimal TripAdvisorRating { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<HotelRoomResponse> HotelRoomResponse { get; set; }
    }
}

