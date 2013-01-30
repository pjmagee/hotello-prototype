namespace Hotello.Services.Wunderground.Weather.Models
{
    public class Station
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Icao { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Neighborhood { get; set; }
        public string Id { get; set; }
        public int DistanceKm { get; set; }
        public int DistanceMi { get; set; }
    }
}