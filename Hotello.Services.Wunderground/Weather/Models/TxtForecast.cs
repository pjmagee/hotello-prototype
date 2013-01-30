using System.Collections.Generic;

namespace Hotello.Services.Wunderground.Weather.Models
{
    public class TxtForecast
    {
        public string Date { get; set; }
        public List<Forecastday> Forecastday { get; set; }
    }
}