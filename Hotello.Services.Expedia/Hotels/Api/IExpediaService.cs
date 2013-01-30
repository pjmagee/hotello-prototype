using Hotello.Services.Expedia.Hotels.Models.Request;
using Hotello.Services.Expedia.Hotels.Models.Response;

namespace Hotello.Services.Expedia.Hotels.Api
{
    public interface IExpediaService
    {
        /// <summary>
        /// <a href="http://api.ean.com/ean-services/rs/hotel/v3/list?">Get List</a>
        /// 
        /// </summary>
        /// <param name="hotelListRequest">The criteria requirements for Hotels</param>
        /// <returns>A list of Hotels</returns>
        HotelListResponse GetHotelAvailabilityList(HotelListRequest hotelListRequest);


        /// <summary>
        /// To obtain a "dateless list," or a list of all active properties in a location without specific availability information, 
        /// simply omit the arrivalDate, departureDate, and RoomGroup parameters from your request.
        /// </summary>
        /// <param name="hotelListRequest"></param>
        /// <returns></returns>
        HotelListResponse GetHotelActiveList(HotelListRequest hotelListRequest);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hotelInformationRequest"></param>
        /// <returns>Information for a Hotel</returns>
        HotelInformationResponse GetHotelInformation(HotelInformationRequest hotelInformationRequest);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomAvailabilityRequest"></param>
        /// <returns>Room availability for a Hotel</returns>
        HotelRoomAvailabilityResponse GetHotelRoomAvailability(HotelRoomAvailabilityRequest roomAvailabilityRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationInfoRequest"></param>
        /// <returns></returns>
        LocationInfoResponse GetGeoSearch(LocationInfoRequest locationInfoRequest);


        /// <summary>
        /// <a href="http://developer.ean.com/docs/hotels/version_3/cancel_reservation/">Cancel Reservation</a>
        /// </summary>
        /// <param name="hotelRoomCancellationRequest"></param>
        /// <returns></returns>
        HotelRoomCancellationResponse GetHotelRoomCancel(HotelRoomCancellationRequest hotelRoomCancellationRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pingRequest"></param>
        /// <returns></returns>
        PingResponse GetPing(PingRequest pingRequest);



    }
}
