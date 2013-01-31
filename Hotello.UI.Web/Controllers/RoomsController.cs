using System;
using System.Linq;
using System.Web.Mvc;
using Hotello.Services.Expedia.Hotels.Api;
using Hotello.Services.Expedia.Hotels.Models;
using Hotello.Services.Expedia.Hotels.Models.Request;
using Hotello.Services.Expedia.Hotels.Models.Response;
using Hotello.UI.Web.Models;
using Ninject;

namespace Hotello.UI.Web.Controllers
{
    public class RoomsController : AbstractExpediaController
    {
        [Inject]
        public RoomsController(AbstractExpediaService expediaService)
        {
            if (expediaService == null)
            {
                throw new ArgumentNullException("expediaService");
            }

            _expediaService = expediaService;
        }

        [HttpGet]
        public ActionResult Availability(int id)
        {
            var model = Session["form"] as SearchViewModel;

            if (model == null)
            {
                // TODO: Please enter desired destination, dates and rooms

                return RedirectToAction("Index", "Search");
            }

            HotelRoomAvailabilityRequest availabilityRequest = new HotelRoomAvailabilityRequest();
            availabilityRequest.HotelId = id;
            availabilityRequest.ArrivalDate = model.CheckinDate;
            availabilityRequest.DepartureDate = model.CheckoutDate;
            availabilityRequest.IncludeRoomImages = true;
            availabilityRequest.IncludeDetails = true;
            availabilityRequest.SupplierType = "E";

            availabilityRequest.RoomGroup = model.RoomViewModels
                .Where(room => room.Adults > 0 || room.Children > 0)
                .Select(room => new Room()
                {
                    NumberOfAdults = room.Adults.HasValue ? room.Adults.Value : 0, // The number of adults requested, or 0
                    NumberOfChildren = room.Children.HasValue ? room.Children.Value : 0, // The number of children requested, or 0
                    ChildAges = room.AgeViewModels.Select(a => a.Age != null ? a.Age.Value : 0).ToList(), // The ages, or 0

                }).ToList();

            HotelRoomAvailabilityResponse hotelRoomAvailabilityResponse = _expediaService.GetHotelRoomAvailability(availabilityRequest);

            if (hotelRoomAvailabilityResponse.EanWsError != null)
            {
                Error(hotelRoomAvailabilityResponse.EanWsError.PresentationMessage);
            }

            Information("Room Availability is currently under construction ;-) ");

            return View(hotelRoomAvailabilityResponse);
        }
    }
}
