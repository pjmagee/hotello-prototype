using System.Collections.Generic;

namespace Hotello.Services.Expedia.Hotels.Models
{
    public class Surcharges
    {
        public int Size { get; set; }
        public List<Surcharge> Surcharge { get; set; }
    }
}