using System.Collections.Generic;

namespace Hotello.Services.Expedia.Hotels.Models
{
    public class PropertyAmenities
    {
        public int Size { get; set; }
        public List<PropertyAmenity> PropertyAmenity { get; set; }
    }
}