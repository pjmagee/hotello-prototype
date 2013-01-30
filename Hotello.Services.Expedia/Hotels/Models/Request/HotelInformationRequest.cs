
using System.Collections.Generic;

namespace Hotello.Services.Expedia.Hotels.Models.Request
{
    public class HotelInformationRequest
    {
        public int HotelId { get; set; }
        public List<Options> Options { get; set; }
    }
}