using System.Collections.Generic;

namespace Hotello.Services.Expedia.Hotels.Models
{
    public class RateInfo
    {
        public bool RateChange { get; set; }
        public bool Promo { get; set; }
        public bool PriceBreakDown { get; set; }
        public List<Room> RoomGroup { get; set; }
        public ChargeableRateInfo ChargeableRateInfo { get; set; }
    }
}