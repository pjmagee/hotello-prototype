using System;

namespace Hotello.Services.Expedia.Hotels.Models
{
    public class HotelSummary
    {
        public int Order { get; set; }
        public string UbsScore { get; set; }
        public int HotelId { get; set; } // hotelId=211540
        public string Name { get; set; } // name=OLIVER PLAZA
        public string Address1 { get; set; } // address1=33 TREBOVIR ROAD
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; } // postalCode=SWSOLR
        public string CountryCode { get; set; } //countryCode=GB
        public string AirportCode { get; set; } // airportCode=LHR
        public int PropertyCategory { get; set; } // propertyCategory=4
        public double HotelRating { get; set; }
        public int ConfidenceRating { get; set; }

        public int AmenityMask { get; set; }

        public Amenities Amenities
        {
            get { return (Amenities) AmenityMask; }
        } 

        public double TripAdvisorRating { get; set; }
        public int TripAdvisorReviewCount { get; set; } // tripAdvisorReviewCount=1439
        public string TripAdvisorRatingUrl { get; set; } // tripAdvisorRatingUrl=http://www.tripadvisor.com/img/cdsi/img2/ratings/traveler/4.0-12345-4.gif
        public string LocationDescription { get; set; } // locationDescription=Near Kensington Palace
        public string ShortDescription { get; set; } // shortDescription=- HOTEL&#x0D;&#x0D;YEAR BUILT -
        public double HighRate { get; set; } // highRate=286.61685
        public double LowRate { get; set; } // lowRate=69.47807
        public string RateCurrencyCode { get; set; }
        public double Latitude { get; set; } // latitude=51.4915
        public double Longitude { get; set; } // longitude=-0.19515
        public double ProximityDistance { get; set; }
        public string ProximityUnit { get; set; } // proximityUnit=MI
        public bool HotelInDestination { get; set; } // hotelInDestination=True
        public string ThumbNailUrl { get; set; } //thumbNailUrl=/hotels/1000000/920000/914300/914263/914263_159_t.jpg
        public string DeepLink { get; set; }
        public RoomRateDetailsList RoomRateDetailsList { get; set; }

    }

    // http://developer.ean.com/general_info/Amenity_Mask
    // The amenityMask is a bitmask of 28 amenities.  
    // Many of the Expedia amenities are mapped into these 28 bitmask amenities and is just a compact way of representing them. 
    // 1 = Business Center, 2 = Fitness Center, 4 = HotTub On-Site, etc.
    // If the amenityMask value is 5, it means that the property has both (1= business center + 4 = Hot Tub OnSite)


    // This was one bloody trickery!!!!

    public class AmenityDescriptionAttribute : Attribute
    {
        private readonly string _value;

        public AmenityDescriptionAttribute(string value)
        {
            this._value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
}