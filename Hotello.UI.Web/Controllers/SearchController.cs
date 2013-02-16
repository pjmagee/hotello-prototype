using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Hotello.Services.Expedia.Hotels.Api;
using Hotello.Services.Expedia.Hotels.Models;
using Hotello.Services.Expedia.Hotels.Models.Request;
using Hotello.Services.Expedia.Hotels.Models.Response;
using Hotello.Services.GeoIp;
using Hotello.UI.Web.Models;
using Ninject;
using PagedList;
using Hotello.UI.Web.Helpers;

namespace Hotello.UI.Web.Controllers
{
    public class SearchController : BaseExpediaController
    {
        private readonly IGeoLookupService _lookUpService;

        [Inject]
        public SearchController(AbstractExpediaService expediaService, IGeoLookupService lookupService)
        {
            if (expediaService == null) // Guard clause
            {
                throw new ArgumentNullException("expediaService");
            }

            if (lookupService == null) // Guard clause
            {
                throw new ArgumentNullException("lookupService");
            }

            _expediaService = expediaService;
            _lookUpService = lookupService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            SearchViewModel model;

            if (Session.IsNewSession)
            {
                model = ModelInitializer.CreateSearchModel();
            }
            else
            {
                model = Session["Search"] as SearchViewModel;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel model)
        {
            if (model.RoomViewModels.Take(model.NumberOfBedrooms).Any(room => !room.Adults.HasValue && !room.Children.HasValue))
            {
                ModelState.AddModelError("RoomViewModels", "Adults and Children required");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Build the request

                    HotelListRequest request = new HotelListRequest();

                    // Arriving on...
                    request.ArrivalDate = new DateTime(model.CheckinDate.Year, model.CheckinDate.Month, model.CheckinDate.Day);
                    // Departing on...
                    request.DepartureDate = new DateTime(model.CheckoutDate.Year, model.CheckoutDate.Month, model.CheckoutDate.Day);
                    
                    // At this address
                    request.City = model.City;
                    request.StateProvinceCode = model.Province;
                    request.CountryCode = model.Country;

                    // Or this location
                    request.DestinationString = model.Destination;

                    // Or hotels around this destination
                    request.DestinationId = model.DestinationId;

                    // With ratings of the following
                    request.MaxStarRating = model.MaximumStarRating;
                    request.MinStarRating = model.MinimumStarRating;

                    // And to cater this many rooms/adults/children
                    request.NumberOfBedRooms = model.NumberOfBedrooms;
                    request.RoomGroup = model.RoomViewModels
                        .Where(room => room.Adults > 0 || room.Children > 0)
                        .Select(room => new Room()
                        {
                            NumberOfAdults = room.Adults.HasValue ? room.Adults.Value : 0,
                            NumberOfChildren = room.Children.HasValue ? room.Children.Value : 0,
                            ChildAges = room.AgeViewModels.Select(a => a.Age != null ? a.Age.Value : 0).ToList(),
                        })
                        .ToList();
                    
                    // Sort the request by prices
                    request.Sort = "PRICE";

                    // Hotel should have these chosen amenities in the results
                    if (model.Amenities != null)
                    {
                        request.Amenities = model.Amenities.Select(a => (Amenity)Enum.Parse(typeof(Amenity), Convert.ToString(a))).ToList();
                    }

                    // Hotel should be this kind of property
                    if (model.PropertyCategories != null)
                    {
                        request.PropertyCategories = model.PropertyCategories.Select(c => (PropertyCategory)Enum.Parse(typeof(PropertyCategory), Convert.ToString(c))).ToList();
                    }


                    // Response
                    HotelListResponse response = _expediaService.GetHotelAvailabilityList(request);

                    if (response.EanWsError != null)
                    {
                        Error(response.EanWsError.PresentationMessage);
                        return View(model);
                    }

                    Session["Search"] = model;
                    Session["Response"] = response;
                    Session["DestinationId"] = model.DestinationId;
                    Session["CustomerSessionId"] = response.CustomerSessionId;
                    Session["MoreResultsAvailable"] = response.MoreResultsAvailable;
                    Session["CacheKey"] = response.CacheKey;
                    Session["CacheLocation"] = response.CacheLocation;

                    return RedirectToAction("Results", "Search");
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            return View(model);
        }


        [HttpGet]
        [OutputCache(Location = OutputCacheLocation.Client, Duration = 60, NoStore = true, VaryByParam = "cacheKey;page")]
        public ActionResult Results(string cacheLocation, string cacheKey, int? page, string sortOrder, string currentFilter, string searchString)
        {
            if (Session["Response"] != null)
            {
                if (Request.Url != null)
                {
                    Session["ResultUrl"] = Request.Url.PathAndQuery;
                }

                HotelListResponse hotelListResponse = Session["Response"] as HotelListResponse;

                if (hotelListResponse != null)
                {
                    // This is where we update our current list of results with more from the remote ip with the specific key
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


                    IEnumerable<HotelSummary> hotelSummaries = hotelListResponse.HotelList.HotelSummary;
                    ViewBag.SortOrder = sortOrder;
                    ViewBag.CurrentFilter = searchString;

                    // Filtering 
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        hotelSummaries = hotelSummaries.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
                    }

                    // Sorting
                    switch (sortOrder)
                    {
                        case "Price desc":
                            hotelSummaries = hotelSummaries
                                .OrderByDescending(s => s.HighRate)
                                .ThenByDescending(s => s.HotelRating);

                            break;

                        case "Price asce":
                            hotelSummaries = hotelSummaries
                                .OrderBy(s => s.HighRate)
                                .ThenByDescending(s => s.HighRate);

                            break;

                        case "Rating desc":
                            hotelSummaries = hotelSummaries
                                .OrderByDescending(s => s.HotelRating)
                                .ThenByDescending(s => s.HighRate);

                            break;

                        case "Rating asce":
                            hotelSummaries = hotelSummaries
                                .OrderBy(s => s.HotelRating)
                                .ThenByDescending(s => s.HighRate);

                            break;

                        default:
                            hotelSummaries = hotelSummaries
                                .OrderBy(s => s.Name)
                                .ThenByDescending(s => s.HighRate);

                            break;
                    }


                    int pageSize = 10;
                    int pageNumber = (page ?? 1);

                    IPagedList<HotelSummary> pagedList = hotelSummaries.ToPagedList(pageNumber, pageSize);

                    if (!pagedList.HasNextPage && (bool)Session["MoreResultsAvailable"])
                    {
                        string info = "<p>Hey, we have more results for you! Would you like to see more results?</p>";
                        info += "<br/>";
                        info += "<a class='btn btn-small btn-info btn-block'";
                        info += " href='" + Url.Action("Results", new { cacheKey = Session["CacheKey"], cacheLocation = Session["CacheLocation"], page = pagedList.PageNumber + 1 }) + "'";
                        info += ">Yes, I want to see more!</a>";
                        
                        Information(new MvcHtmlString(info));
                    }

                    return View("Results", pagedList);
                }    
            }
            return RedirectToAction("Index");
        }

        [Ajax]
        [HttpGet]
        [OutputCache(Location = OutputCacheLocation.ServerAndClient, Duration = 60, NoStore = true, VaryByParam = "destinationString")]
        public JsonResult Landmarks(string destinationString)
        {
            LocationInfoRequest request = new LocationInfoRequest();
            request.DestinationString = destinationString;

            try
            {
                LocationInfoResponse response = _expediaService.GetGeoSearch(request);

                var landmarks = response.LocationInfos.LocationInfo
                    .Where(info => info.Active && info.GeoAccuracy >= 1 && info.LocationInDestination && info.ActivePropertyCount > 0)
                        .Select(info => new
                        {
                            destinationId = info.DestinationId,
                            description = info.Description
                        })
                        .OrderBy(arg => arg.description) // Order by landmark description
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
