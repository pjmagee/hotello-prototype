using Newtonsoft.Json;

namespace Hotello.Services.Expedia.Hotels.Models
{
    [JsonObject]
    public class BedType
    {
        [JsonProperty("@id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}