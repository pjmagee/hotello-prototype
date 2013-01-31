using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Hotello.Services.Expedia.Hotels.Api;
using Hotello.Services.Expedia.Hotels.Models;
using Hotello.Services.Expedia.Hotels.Models.Request;
using Hotello.Services.Expedia.Hotels.Models.Response;
using Hotello.Services.GeoIp;
using Ninject;

namespace Hotello.UI.Web.Controllers
{
    public class HotelsController : AbstractExpediaController
    {
        private readonly IGeoLookupService _lookUpService;

        [Inject]
        public HotelsController(AbstractExpediaService expediaService, IGeoLookupService geoLookupService)
        {
            if (expediaService == null)
            {
                throw new ArgumentNullException("expediaService");
            }

            if (geoLookupService == null)
            {
                throw new ArgumentNullException("geoLookupService");
            }

            _lookUpService = geoLookupService;
            _expediaService = expediaService;
        }

        [HttpGet]
        public ActionResult Information(int id)
        {
            HotelInformationRequest hotelInformationRequest = new HotelInformationRequest();
            hotelInformationRequest.Options = new List<Options>();
            hotelInformationRequest.HotelId = id;

            HotelInformationResponse hotelInformationResponse = _expediaService.GetHotelInformation(hotelInformationRequest);

            if (hotelInformationResponse.EanWsError != null)
            {
                // TODO: Complete Ean Ws Error Handling
            }

            return View(hotelInformationResponse);
        }

        [HttpGet]
        public ActionResult Images(int id)
        {
            HotelInformationRequest request = new HotelInformationRequest();
            request.HotelId = id;
            request.Options = new List<Options>(){ Options.HOTEL_IMAGES };

            HotelInformationResponse response = _expediaService.GetHotelInformation(request);

            if (response.EanWsError != null)
            {
                // TODO: Complete Ean Ws Error Handling
            }

            return Json(response.HotelImages.HotelImage, JsonRequestBehavior.AllowGet);
        }
    }
}
