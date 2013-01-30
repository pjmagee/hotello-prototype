using System.Collections.Generic;

namespace Hotello.Services.Wunderground.Weather.Models
{
    public class Simpleforecast
    {
        public List<Forecastday> Forecastday { get; set; }
    }
}