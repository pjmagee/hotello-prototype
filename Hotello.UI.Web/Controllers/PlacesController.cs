using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hotello.Services.Google.Places.Api;
using Hotello.Services.Google.Places.Models.Autocomplete;
using Hotello.UI.Web.Attributes;
using Hotello.UI.Web.Helpers;
using Ninject;

namespace Hotello.UI.Web.Controllers
{
    /// <summary>
    /// Google Places API Service Consumer
    /// </summary>
    public class PlacesController : Controller
    {
        private readonly IPlacesService _placesService;

        [Inject]
        public PlacesController(IPlacesService placesService)
        {
            if (placesService == null)
            {
                throw new ArgumentNullException("placesService");
            }

            _placesService = placesService;
        }

        [Ajax]
        [HttpGet]
        public JsonResult AutoSuggestDestination(string text)
        {
            // https://maps.googleapis.com/maps/api/place/autocomplete/json?input=Car&sensor=false&key=AIzaSyDm7lXUQTaEvPo-eghcFIo2VjJvyKawKUo

            AutocompletionRequest autocompletionRequest = new AutocompletionRequest();
            autocompletionRequest.Sensor = false;
            autocompletionRequest.Input = text;
            autocompletionRequest.Types = "geocode";
            AutocompletionResponse autocompletionResponse = _placesService.Autocomplete(autocompletionRequest);

            var terms = autocompletionResponse.Predictions.Select(prediction => new { destination = prediction.Description, suggestion = prediction.Description });

            return Json(terms.ToList(), JsonRequestBehavior.AllowGet);
        }

        [Ajax]
        [HttpGet]
        public JsonResult AutoSuggestCity(string text)
        {
            AutocompletionRequest autocompletionRequest = new AutocompletionRequest();
            autocompletionRequest.Sensor = false;
            autocompletionRequest.Input = text;
            AutocompletionResponse autocompletionResponse = _placesService.Autocomplete(autocompletionRequest);

            // You can see how the first value is most relevant, all we need to do is check the type is "locality" or "sublocality"
            // https://maps.googleapis.com/maps/api/place/autocomplete/json?input=Car&sensor=false&key=AIzaSyDm7lXUQTaEvPo-eghcFIo2VjJvyKawKUo

            IEnumerable<Prediction> predictions = autocompletionResponse.Predictions
                .Where(prediction => prediction.Types.Any(type => (type.Equals("sublocality") || type.Equals("locality"))));

            var terms = predictions.Select(prediction => new
            {
                description = prediction.Description,
                suggestion = prediction.Terms.First().Value,
            });

            return Json(terms.ToList(), JsonRequestBehavior.AllowGet);
        }

        [Ajax]
        [HttpGet]
        public JsonResult AutoSuggestCounty(string text)
        {
            AutocompletionRequest autocompletionRequest = new AutocompletionRequest();
            autocompletionRequest.Sensor = false;
            autocompletionRequest.Input = text;
            AutocompletionResponse autocompletionResponse = _placesService.Autocomplete(autocompletionRequest);

            // You can see how the first value is most relevant, all we need to do is check the type is "administrative_area_level_2"
            // This should be our county / province 
            // https://maps.googleapis.com/maps/api/place/autocomplete/json?input=Surrey&sensor=false&key=AIzaSyDm7lXUQTaEvPo-eghcFIo2VjJvyKawKUo

            IEnumerable<Prediction> predictions = autocompletionResponse.Predictions.Where(prediction => prediction.Types.Any(type => type.Equals("administrative_area_level_2")));
            var terms = predictions.Select(prediction => new { suggestion = prediction.Terms.First().Value, description = prediction.Description });

            return Json(terms.ToList(), JsonRequestBehavior.AllowGet);

        }

    }
}
