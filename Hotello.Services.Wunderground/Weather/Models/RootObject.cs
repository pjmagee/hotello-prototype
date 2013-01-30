namespace Hotello.Services.Wunderground.Weather.Models
{
    public class RootObject
    {
        public Response Response { get; set; }
        public Location Location { get; set; }
        public Forecast Forecast { get; set; }
    }
}