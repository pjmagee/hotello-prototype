using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hotello.Services.Expedia.Hotels.Models
{
    [JsonObject]
    public class BedTypes
    {
        public int Size { get; set; }

        [JsonProperty("BedType")]
        public List<BedType> BedType { get; set; }
    }
}