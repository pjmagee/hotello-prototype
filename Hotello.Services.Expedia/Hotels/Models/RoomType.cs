using System.Collections.Generic;

namespace Hotello.Services.Expedia.Hotels.Models
{
    public class RoomType
    {
        public string RoomCode { get; set; }
        public int RoomTypeId { get; set; }
        public string Description { get; set; }
        public string DescriptionLong { get; set; }
        public RoomAmenities RoomAmenities { get; set; }
    }
}