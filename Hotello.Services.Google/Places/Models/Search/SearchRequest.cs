namespace Hotello.Services.Google.Places.Models.Search
{
    public class SearchRequest
    {
        public bool IsSensor { get; set; }
        public string Location { get; set; }
        public decimal Radius { get; set; }
        public string Language { get; set; }
    }
}