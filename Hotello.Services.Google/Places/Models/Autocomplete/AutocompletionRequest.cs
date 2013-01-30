namespace Hotello.Services.Google.Places.Models.Autocomplete
{

    // https://developers.google.com/places/documentation/autocomplete#place_autocomplete_requests

    public class AutocompletionRequest
    {
        public string Input { get; set; }
        public bool Sensor { get; set; }
        public int Offset { get; set; }
        public string Location { get; set; }
        public int Radius { get; set; }
        public string Language { get; set; }
        public string Components { get; set; }
        public string Types { get; set; }
    }
}
