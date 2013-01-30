namespace Hotello.Services.GeoIp
{
    public class GeoLookUpResponse
    {
        public string City { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string Metrocode { get; set; }
        public string Zipcode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string CountryCode { get; set; }
        public string Ip { get; set; }
        public string CountryName { get; set; }
    }
}