using System.Collections.Generic;

namespace Hotello.Services.Google.DistanceMatrix.Models
{
    public class DistanceMatrixResponse
    {
        public string Status { get; set; }
        public List<string> OriginAddresses { get; set; }
        public List<string> DestinationAddresses { get; set; }
        public List<Row> Rows { get; set; }
    }
}