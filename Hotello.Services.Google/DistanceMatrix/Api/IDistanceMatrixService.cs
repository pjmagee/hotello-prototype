using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotello.Services.Google.DistanceMatrix.Models;

namespace Hotello.Services.Google.DistanceMatrix.Api
{
    /// <summary>
    /// https://developers.google.com/maps/documentation/distancematrix/
    ///  
    /// <remarks>
    /// Use of the Distance Matrix API must relate to the display of information on a Google Map; 
    /// for example, to determine origin-destination pairs that fall within a specific driving time from one another, 
    /// before requesting and displaying those destinations on a map. Use of the service in an application that doesn't display a Google map is prohibited.
    /// </remarks>
    /// </summary>
    public interface IDistanceMatrixService
    {

        /// <summary>
        /// https://developers.google.com/maps/documentation/distancematrix/#DistanceMatrixRequests
        /// </summary>
        /// <param name="distanceMatrixRequest"></param>
        /// <returns></returns>
        DistanceMatrixResponse GetDistanceMatrix(DistanceMatrixRequest distanceMatrixRequest);
    }
}
