using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Hotello.Services.Expedia.Hotels.Models
{
    public class Room
    {
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public List<int> ChildAges { get; set; }
        public string RateKey { get; set; }
    }
}