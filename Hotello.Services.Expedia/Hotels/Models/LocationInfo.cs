namespace Hotello.Services.Expedia.Hotels.Models
{

    /*
           {
                "destinationId" : "61361E78-F6BB-4CAE-9857-821C80A2FEF9",
                "active" : true,
                "type" : 1,
                "city" : "East Las Vegas",
                "stateProvinceCode" : "NV",
                "countryCode" : "US",
                "countryName" : "United States",
                "code" : "East Las Vegas,NV,US",
                "description" : "East Las Vegas",
                "geoAccuracy" : 1,
                "locationInDestination" : false,
                "latitude" : 36.121315,
                "longitude" : -115.06261,
                "refLocationMileage" : 0
            }
     */



    public class LocationInfo
    {
        public string DestinationId { get; set; }

        public bool Active { get; set; }

        public int ActivePropertyCount { get; set; }

        public int Type { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string StateProvinceCode { get; set; }

        public string PostalCode { get; set; }

        public string Description { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public string Code { get; set; }

        public int GeoAccuracy { get; set; }

        public bool LocationInDestination { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double RefLocationMileage { get; set; }

    }


}