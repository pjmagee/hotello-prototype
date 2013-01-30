using System;
using System.Collections.Generic;
using Hotello.Services.Expedia.Hotels.Models.Request;

namespace Hotello.Services.Expedia.Hotels.Models.Response
{
    /// <summary>
    /// Each node contains rates and details for an individual room at the hotel. 
    /// The JSON response format of this array may cause issues in Axis.
    /// 
    /// Well I don't use Apache Axis, so i guess im lucky :)
    /// </summary>
    public class HotelRoomResponse
    {
        public EanWsError EanWsError { get; set; }

        /// <summary>
        /// Additional miscellaneous policies, e.g. photo ID required for check-in.
        /// </summary>
        public string Policy { get; set; }

        /// <summary>
        /// Code to the displayed rate. 
        /// For Hotel Collect listings, special requirements such as AAA or AARP membership are determined via this or the element.
        /// </summary>
        public long RateCode { get; set; }


        /// <summary>
        /// Room type code for the room. 
        /// If the options parameter was sent in the room availability request, this element will not return.  
        /// The value is instead sent as the roomCode attribute of the RoomType object.
        /// </summary>
        public string RoomTypeCode { get; set; }

        /// <summary>
        /// Required only for Hotel Collect rooms. Description of the rate to be charged. 
        /// Must display on all Hotel Collect room, booking, and booking confirmation pages.
        /// </summary>
        public string RateDescription { get; set; }

        /// <summary>
        /// Short description of the room type, e.g. Deluxe Room King. 
        /// Must display on individual room pages as well as any booking and booking confirmation pages.
        /// </summary>
        public string RoomTypeDescription { get; set; }

        /// <summary>
        /// Supplier of the hotel. This same supplier will be used to process any reservations placed.
        /// Use this element to determine proper text for pricing Expedia Collect and Hotel Collect listings.
        /// E: Expedia Collect
        /// V: Venere (Hotel Collect )
        /// S: Sabre (Hotel Collect )
        /// W: Worldspan (Hotel Collect )
        /// </summary>
        public string SuppplierType { get; set; }


        /// <summary>
        /// Other miscellaneous info on the hotel, if available.
        /// </summary>
        public string OtherInformation { get; set; }

        /// <summary>
        /// Useful for Hotel Collect only. 
        /// Expedia Collect always returns as true since the reservation is always prepaid.
        /// 
        /// Use as a check against other parameters to verify if it is appropriate to advise 
        /// customers of an immediate charge to their credit card once the reservation is submitted. 
        /// Not returned in minorRev=20 and above.
        /// </summary>
        public bool ImmediateChargeRequired { get; set; }

        /// <summary>
        /// Expedia's ID for the hotel. 
        /// Use this value to map to a hotelId when cross-referencing to Expedia. 
        /// A complete cross-reference file is also available in the database catalog.
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Available smoking preferences for the room, if any.
        /// Values: 
        /// NS: non-smoking
        /// S: smoking
        /// E: either
        /// </summary>
        public string SmokingPreferences { get; set; }

        /// <summary>
        /// The minimum age of any guests in the room, e.g. 18 or 21 if children are not allowed.
        /// Will return as 0 or not at all if there is no minimum age.
        /// </summary>
        public int MinGuestAge { get; set; }

        /// <summary>
        /// The maximum number of guests the room can accommodate.
        /// </summary>
        public int MaxRoomOccupancy { get; set; }

        /// <summary>
        /// Guest count upon which the provided room rate is based. 
        /// </summary>
        public int QuotedOccupancy { get; set; }

        /// <summary>
        /// Indicates how many guests the room can accomodate for the provided rate. 
        /// </summary>
        public int RateOccupancyPerRoom { get; set; }

        /// <summary>
        /// Well, Hotello is not a hybrid site, eventually - realistically, it should even handle its own booking procedures
        /// But i cant do this on my own. Just not plausabible regarding caching, database caching, request / response
        /// cacheing, database catalog weekly imports / updates of database data.
        /// 
        /// Deep link into the corresponding room availability page on your template, 
        /// used if you are creating a hybrid site. 
        /// </summary>
        public string DeepLink { get; set; }

        /// <summary>
        /// The bed type choices for the individual room. 
        /// May return a single bed type or a choice to include at booking time. 
        /// Review details for bed types
        /// 
        /// http://developer.ean.com/general_info/BedTypes
        /// </summary>
        public List<BedType> BedTypes { get; set; }

        /// <summary>
        /// ValueAdd container / wrapper object
        /// 
        /// Contains an array of ValueAdd elements, if available, for the provided room and rate.
        /// Has a size attribute to indicate the number of value adds returned.
        /// 
        /// </summary>
        public ValueAdds ValueAdds { get; set; }

        /// <summary>
        /// Array of any available room-level image URLs, if requested via the includeRoomImages parameter.
        /// </summary>
        public RoomImages RoomImages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CancellationPolicy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool NonRefundable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool GuaranteeRequired { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DepositRequired { get; set; }
        

        /// <summary>
        /// 
        /// </summary>
        public int CurrentAllotment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long PromoId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PromoDescription { get; set; }
        
        /// <summary>
        /// /
        /// </summary>
        public CancelPolicyInfoRequest CancelPolicyInfoRequest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RateInfos RateInfos { get; set; }
        
         
        
    }
}