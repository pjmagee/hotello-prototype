using System.Collections.Generic;

namespace Hotello.Services.Expedia.Hotels.Models.Response
{
    public class LocationInfoResponse
    {
        public EanWsError EanWsError { get; set; }
        public string CustomerSessionId { get; set; }
        public LocationInfos LocationInfos { get; set; }
    }
}