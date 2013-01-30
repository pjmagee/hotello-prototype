using System.Collections.Generic;

namespace Hotello.Services.Google.Places.Models.Autocomplete
{
    public class AutocompletionResponse
    {
        public string Status { get; set; }
        public List<Prediction> Predictions { get; set; }
    }

    public class Prediction
    {
        public string Description { get; set; }
        public List<string> Types { get; set; }
        public List<Term> Terms { get; set; }
    }

    public class Term
    {
        public string Value { get; set; }
        public int Offset { get; set; }
    }
}