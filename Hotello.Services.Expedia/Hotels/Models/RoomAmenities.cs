using System.Collections.Generic;

namespace Hotello.Services.Expedia.Hotels.Models
{
    public class RoomAmenities
    {
        public int Size { get; set; }
        public List<RoomAmenity> RoomAmenity { get; set; }
    }
}