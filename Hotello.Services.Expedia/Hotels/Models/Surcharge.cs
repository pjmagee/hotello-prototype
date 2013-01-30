using System.Collections.Generic;

namespace Hotello.Services.Expedia.Hotels.Models
{
    public class Surcharge
    {
        public decimal Amount { get; set; }
        public string Type { get; set; }
    }

    public class Surcharges
    {
        public int Size { get; set; }
        public List<Surcharge> Surcharge { get; set; }
    }
}