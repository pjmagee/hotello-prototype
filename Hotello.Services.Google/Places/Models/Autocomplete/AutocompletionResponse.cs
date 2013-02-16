using System.Collections.Generic;

namespace Hotello.Services.Google.Places.Models.Autocomplete
{
    public class AutocompletionResponse
    {
        public string Status { get; set; }
        public List<Prediction> Predictions { get; set; }
    }
}