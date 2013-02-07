using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using Hotello.Services.Expedia.Hotels.Api;
using Hotello.Services.Expedia.Hotels.Models;
using Hotello.Services.Expedia.Hotels.Models.Request;
using Hotello.Services.Expedia.Hotels.Models.Response;
using Hotello.Services.GeoIp;
using Hotello.UI.Web.Attributes;
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
        [OutputCache(Duration = 60 * 5, Location = OutputCacheLocation.ServerAndClient, NoStore = true, VaryByParam = "id")]
        public ActionResult Information(int id, string name)
        {
           HotelInformationRequest hotelInformationRequest = new HotelInformationRequest
                {
                    Options = new List<Options>(),
                    HotelId = id
                };

            HotelInformationResponse response = _expediaService.GetHotelInformation(hotelInformationRequest);

            if (response.EanWsError != null)
            {
               Error(response.EanWsError.PresentationMessage);
            }

            if(string.IsNullOrEmpty(name))
            {
                name = response.HotelSummary.Name;
            }
            
            return View(response);
        }


        [Ajax]
        [HttpGet]
        [OutputCache(Duration = 60 * 5, Location = OutputCacheLocation.ServerAndClient, NoStore = true, VaryByParam = "id")]
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
