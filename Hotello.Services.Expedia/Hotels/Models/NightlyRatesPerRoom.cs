using System.Collections.Generic;

namespace Hotello.Services.Expedia.Hotels.Models
{
    public class NightlyRatesPerRoom
    {
        public int Size { get; set; }
        public List<NightlyRate> NightlyRate { get; set; }
    }
}