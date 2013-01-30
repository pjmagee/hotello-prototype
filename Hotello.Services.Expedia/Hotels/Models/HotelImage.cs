namespace Hotello.Services.Expedia.Hotels.Models
{
    public class HotelImage
    {
        public int HotelImageId { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public int Type { get; set; }
        public string Caption { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        public int SupplierId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ByteSize { get; set; }
    }
}