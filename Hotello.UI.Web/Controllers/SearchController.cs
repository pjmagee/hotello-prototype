using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Hotello.Services.Expedia.Hotels.Api;
using Hotello.Services.Expedia.Hotels.Models;
using Hotello.Services.Expedia.Hotels.Models.Request;
using Hotello.Services.Expedia.Hotels.Models.Response;
using Hotello.Services.GeoIp;
using Hotello.UI.Web.Helpers;
using Hotello.UI.Web.Models;
using Ninject;
using PagedList;

namespace Hotello.UI.Web.Controllers
{
    public class SearchController : AbstractExpediaController
    {
        private readonly IGeoLookupService _lookUpService;

        [Inject]
        public SearchController(AbstractExpediaService expediaService, IGeoLookupService lookupService)
        {
            if (expediaService == null)
            {
                throw new ArgumentNullException("expediaService");
            }

            _expediaService = expediaService;
            _lookUpService = lookupService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new SearchViewModel
            {
                NumberOfBedrooms = 1, // Default to 1 bedroom?
                RoomViewModels = new List<RoomViewModel>(Enumerable.Range(1, 4) // From 1 to 4 rooms
                    .Select(room => new RoomViewModel()
                    {
                        Adults = null,
                        Children = null,
                        AgeViewModels = new List<AgeViewModel>(Enumerable.Range(1, 3).Select(i =>
                            new AgeViewModel()
                            {
                                Age = null,
                            }))
                    })),
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Build the request

                    HotelListRequest request = new HotelListRequest();
                    request.DepartureDate = model.DepartureDate;
                    request.ArrivalDate = model.ArrivalDate;
                    request.City = model.City;
                    request.StateProvinceCode = model.Province;
                    request.CountryCode = model.Country;
                    request.DestinationString = model.Destination;
                    request.DestinationId = model.DestinationId;
                    request.MaxStarRating = model.MaximumStarRating;
                    request.MinStarRating = model.MinimumStarRating;
                    request.NumberOfBedRooms = model.NumberOfBedrooms;
                    request.Sort = "PRICE";

                    if (model.Amenities != null)
                    {
                        request.Amenities = model.Amenities.Select(a => (Amenity)Enum.Parse(typeof(Amenity), Convert.ToString(a))).ToList();
                    }

                    if (model.PropertyCategories != null)
                    {
                        request.PropertyCategories = model.PropertyCategories.Select(c => (PropertyCategory)Enum.Parse(typeof(PropertyCategory), Convert.ToString(c))).ToList();
                    }

                    request.RoomGroup = model.RoomViewModels
                        .Where(room => room.Adults > 0 || room.Children > 0)
                        .Select(room => new Room()
                        {
                            NumberOfAdults = room.Adults.HasValue ? room.Adults.Value : 0,
                            NumberOfChildren = room.Children.HasValue ? room.Children.Value : 0,
                            ChildAges = room.AgeViewModels.Select(a => a.Age != null ? a.Age.Value : 0).ToList(),
                        })
                        .ToList();

                    // Response

                    HotelListResponse response = _expediaService.GetHotelAvailabilityList(request);

                    if (response.EanWsError != null)
                    {
                        //TODO:  Handle Ean Ws Error
                        return HttpNotFound(response.EanWsError.PresentationMessage);
                    }

                    Session.Timeout = 10;
                    Session["form"] = model;
                    Session["Response"] = response;
                    Session["CustomerSessionId"] = response.CustomerSessionId;
                    Session["MoreResultsAvailable"] = response.MoreResultsAvailable;
                    Session["CacheKey"] = response.CacheKey;
                    Session["CacheLocation"] = response.CacheLocation;

                    return View("Results", response.HotelList.HotelSummary.ToPagedList(1, 10));
                }
                catch (Exception)
                {

                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Results(string cacheLocation, string cacheKey, int? page)
        {
            if (Session["Response"] != null)
            {
                HotelListResponse hotelListResponse = Session["Response"] as HotelListResponse;

                if (hotelListResponse != null)
                {
                    if (cacheKey != null && cacheLocation != null)
                    {
                        HotelListRequest request = new HotelListRequest();
                        request.CacheKey = cacheKey;
                        request.CacheLocation = cacheLocation;

                        var pagedResponseResults = _expediaService.GetHotelAvailabilityList(request);

                        Session["CustomerSessionId"] = pagedResponseResults.CustomerSessionId;
                        Session["MoreResultsAvailable"] = pagedResponseResults.MoreResultsAvailable;
                        Session["CacheKey"] = pagedResponseResults.CacheKey;
                        Session["CacheLocation"] = pagedResponseResults.CacheLocation;

                        if (pagedResponseResults.HotelList != null && pagedResponseResults.HotelList.HotelSummary != null)
                        {
                            // Append paged results
                            hotelListResponse.HotelList.HotelSummary.AddRange(pagedResponseResults.HotelList.HotelSummary);
                        }
                    }

                    int pageSize = 10;
                    int pageNumber = (page ?? 1);

                    return View("Results", hotelListResponse.HotelList.HotelSummary.ToPagedList(pageNumber, pageSize));
                }    
            }
            return View();
        }

        [Ajax]
        [HttpGet]
        public JsonResult Landmarks(string destinationString)
        {
            LocationInfoRequest infoRequest = new LocationInfoRequest();
            infoRequest.DestinationString = destinationString;

            try
            {
                LocationInfoResponse locationInfoResponse = _expediaService.GetGeoSearch(infoRequest);

                var landmarks = locationInfoResponse.LocationInfos.LocationInfo
                    .Where(info => info.Active && info.GeoAccuracy >= 1 && info.LocationInDestination && info.ActivePropertyCount > 0)
                        .Select((info) => new
                        {
                            destinationId = info.DestinationId,
                            description = info.Description
                        })
                        .ToList();

                return Json(landmarks, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return new JsonResult();

        }
    }
}
