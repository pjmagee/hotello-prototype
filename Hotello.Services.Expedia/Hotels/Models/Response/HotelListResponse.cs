namespace Hotello.Services.Expedia.Hotels.Models.Response
{
    public class HotelListResponse
    {
        public EanWsError EanWsError { get; set; }
        public CachedSupplierResponse CachedSupplierResponse { get; set; }
        public string CacheKey { get; set; }
        public string CacheLocation { get; set; }
        public string CustomerSessionId { get; set; }
        public HotelList HotelList { get; set; }
        public bool MoreResultsAvailable { get; set; }
        public int NumberOfRoomsRequested { get; set; } 
    }
}
