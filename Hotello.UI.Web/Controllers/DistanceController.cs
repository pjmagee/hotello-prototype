using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Hotello.Services.Expedia.Hotels.Api;
using Hotello.Services.Expedia.Hotels.Models;
using Hotello.Services.Expedia.Hotels.Models.Request;
using Hotello.Services.Expedia.Hotels.Models.Response;
using Hotello.Services.Google.DistanceMatrix.Api;
using Hotello.Services.Google.DistanceMatrix.Models;
using Hotello.UI.Web.Helpers;
using Hotello.UI.Web.Models;
using Ninject;

namespace Hotello.UI.Web.Controllers
{
    public class DistanceController : Controller
    {
        private readonly AbstractExpediaService _expediaService;
        private readonly IDistanceMatrixService _distanceMatrixService;

        [Inject]
        public DistanceController(IDistanceMatrixService distanceMatrixService, AbstractExpediaService expediaService)
        {
            if (distanceMatrixService == null)
            {
                throw new ArgumentNullException("distanceMatrixService");
            }

            if (expediaService == null)
            {
                throw new ArgumentNullException("expediaService");
            }

            _expediaService = expediaService;
            _distanceMatrixService = distanceMatrixService;
        }


        [Ajax]
        [HttpPost]
        [OutputCache(Duration = 60, NoStore = true, VaryByParam = "destinationId;page", Location = OutputCacheLocation.ServerAndClient)]
        public JsonResult Index(List<HotelDistanceViewModel> hotels, string destinationId, int? page)
        {
            // Information about the Destination Id,
            // This will return Lat/Lon of the destination ID
            LocationInfoRequest locationInfoRequest = new LocationInfoRequest() {DestinationId = destinationId};
            LocationInfoResponse locationInfoResponse = _expediaService.GetGeoSearch(locationInfoRequest);


            if (locationInfoResponse != null)
            {
                if (locationInfoResponse.EanWsError == null)
                {
                    if (locationInfoResponse.LocationInfos.LocationInfo.Any(info => info.DestinationId == destinationId))
                    {
                        var destinationInfo = locationInfoResponse.LocationInfos.LocationInfo.First(info => info.DestinationId == destinationId);
                                               
                        var projection = hotels.Select(hotel => new
                            {
                                HotelId = hotel.HotelId,
                                Distance = GetDistance(hotel, destinationInfo)
                            });

                        return Json(new 
                        { 
                            Landmark = destinationInfo.Description,
                            Hotels = projection.ToList() 
                        });
                    }
                }
            }

            return new JsonResult();
        }


        private string GetDistance(HotelDistanceViewModel hotel, LocationInfo info)
        {
            DistanceMatrixRequest request = new DistanceMatrixRequest();

            // Origins would be the hotel
            request.Origins = new List<string>();
            request.Origins.Add(string.Format("{0},{1}", hotel.Latitude, hotel.Longitude));

            request.Destinations = new List<string>();
            request.Destinations.Add(string.Format("{0},{1}", info.Latitude, info.Longitude));

            DistanceMatrixResponse response = _distanceMatrixService.GetDistanceMatrix(request);
            
            return response.Rows.First().Elements.First().Distance.Text;
        }
    }
}
