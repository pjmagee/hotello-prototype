using System.Collections.Generic;

namespace Hotello.Services.Expedia.Hotels.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public List<int> ChildAges { get; set; }
    }

    public class RoomImage
    {
        public string Url { get; set; }
    }

    public class RoomImages
    {
        public int Size { get; set; }
        public List<RoomImage> RoomImage { get; set; }
    }


}