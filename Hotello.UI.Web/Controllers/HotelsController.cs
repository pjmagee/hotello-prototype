using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.UI;
using Hotello.Services.Expedia.Hotels.Api;
using Hotello.Services.Expedia.Hotels.Models;
using Hotello.Services.Expedia.Hotels.Models.Request;
using Hotello.Services.Expedia.Hotels.Models.Response;
using Hotello.Services.GeoIp;
using Hotello.UI.Web.Helpers;
using Ninject;

namespace Hotello.UI.Web.Controllers
{
    /// <summary>
    /// The Hotels Controller 
    /// Consumes the Expedia and Geo Lookup Services 
    /// Provides the following functionality: 
    /// Retrieve images for a particular hotel
    /// Retrieve detailed information for a particular hotel
    /// </summary>
    public class HotelsController : BaseExpediaController
    {
        private readonly IGeoLookupService _lookUpService;

        [Inject]
        public HotelsController(AbstractExpediaService expediaService, IGeoLookupService geoLookupService)
        {
            if (expediaService == null) // Guard Clause
            {
                throw new ArgumentNullException("expediaService");
            }

            if (geoLookupService == null) // Guard Clause
            {
                throw new ArgumentNullException("geoLookupService");
            }

            _lookUpService = geoLookupService;
            _expediaService = expediaService;
        }

        [HttpGet]
        [OutputCache(Duration = 30, Location = OutputCacheLocation.Client, NoStore = true, VaryByParam = "id")]
        public ActionResult Information(int id, string name)
        {
            HotelInformationRequest request = new HotelInformationRequest
                {
                    Options = new List<Options>(),
                    HotelId = id
                };

            try
            {
                HotelInformationResponse response = _expediaService.GetHotelInformation(request);

                if (response.EanWsError != null)
                {
                    Error(response.EanWsError.PresentationMessage);
                }

                return View(response);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                
                Error("An error occured. Please try again later.");
            }
            
            return View();
        }


        [Ajax]
        [HttpGet]
        [OutputCache(Duration = 60 * 5, Location = OutputCacheLocation.ServerAndClient, NoStore = true, VaryByParam = "id")]
        public JsonResult Images(int id)
        {
            HotelInformationRequest request = new HotelInformationRequest
                {
                    HotelId = id,
                    Options = new List<Options>()
                        {
                            Options.HOTEL_IMAGES
                        }
                };

            HotelInformationResponse response = _expediaService.GetHotelInformation(request);

            if (response.EanWsError != null)
            {
                Error(response.EanWsError.PresentationMessage);
            }

            return Json(response.HotelImages.HotelImage, JsonRequestBehavior.AllowGet);
        }
    }
}
