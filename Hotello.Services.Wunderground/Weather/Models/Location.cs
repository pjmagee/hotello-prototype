namespace Hotello.Services.Wunderground.Weather.Models
{
    public class Location
    {
        public string Type { get; set; }
        public string Country { get; set; }
        public string CountryIso3166 { get; set; }
        public string CountryName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string TzShort { get; set; }
        public string TzLong { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Zip { get; set; }
        public string Magic { get; set; }
        public string Wmo { get; set; }
        public string L { get; set; }
        public string Requesturl { get; set; }
        public string Wuiurl { get; set; }
        public NearbyWeatherStations NearbyWeatherStations { get; set; }
    }
}