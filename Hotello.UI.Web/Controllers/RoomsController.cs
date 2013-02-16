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
    /// <summary>
    /// The Rooms controllers provides information about
    /// room availability for a given hotel.
    /// Room Images for a given hotel
    /// </summary>
    public class RoomsController : BaseExpediaController
    {
        [Inject]
        public RoomsController(AbstractExpediaService expediaService)
        {
            if (expediaService == null) // Guard Clause
            {
                throw new ArgumentNullException("expediaService");
            }

            _expediaService = expediaService;
        }

        [HttpGet]
        public ActionResult Availability(int id)
        {
            var model = Session["Search"] as SearchViewModel;

            if (model == null)
            {
                //Information("Please enter your criteria for room availability");
                //return RedirectToAction("Index", "Search");
                model = ModelInitializer.CreateSearchModel();
                model.CheckinDate = DateTime.Today.AddDays(1);
                model.CheckoutDate = model.CheckinDate.AddDays(7);
                model.RoomViewModels.First().Adults = 1;
                model.RoomViewModels.First().Children = 0;

                Session["Search"] = model;
            }
            
            HotelRoomAvailabilityRequest request = new HotelRoomAvailabilityRequest();
            request.HotelId = id;
            request.ArrivalDate = model.CheckinDate;
            request.DepartureDate = model.CheckoutDate;
            request.IncludeRoomImages = true;
            request.IncludeDetails = true;
            request.SupplierType = "E";
            request.NumberOfBedrooms = model.NumberOfBedrooms;
            request.RoomGroup = model.RoomViewModels
                        .Where(room => room.Adults > 0 || room.Children > 0)
                        .Select(room => new Room()
                        {
                            NumberOfAdults = room.Adults.HasValue ? room.Adults.Value : 0,
                            NumberOfChildren = room.Children.HasValue ? room.Children.Value : 0,
                            ChildAges = room.AgeViewModels.Select(a => a.Age != null ? a.Age.Value : 0).ToList(),
                        })
                        .ToList();

            HotelRoomAvailabilityResponse response = _expediaService.GetHotelRoomAvailability(request);

            if (response.EanWsError != null)
            {
                Error(response.EanWsError.PresentationMessage);
            }

            return View(response);
        }
    }
}
