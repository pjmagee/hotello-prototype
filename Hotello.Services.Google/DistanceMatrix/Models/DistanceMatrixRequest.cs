using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotello.Services.Google.DistanceMatrix.Models
{
    /// <summary>
    /// https://developers.google.com/maps/documentation/distancematrix/#DistanceMatrixRequests
    /// </summary>
    public class DistanceMatrixRequest
    {
        #region Required parameters

        /// <summary>
        /// origins — One or more addresses and/or textual latitude/longitude values, 
        /// separated with the pipe (|) character, 
        /// from which to calculate distance and time.
        /// 
        /// If you pass an address as a string, the service will geocode the string and convert it to a latitude/longitude coordinate to calculate directions.
        /// If you pass coordinates, ensure that no space exists between the latitude and longitude values.
        /// </summary>
        public List<string> Origins { get; set; }


        /// <summary>
        /// destinations — One or more addresses and/or textual latitude/longitude values, 
        /// separated with the pipe (|) character, 
        /// to which to calculate distance and time. 
        /// 
        /// If you pass an address as a string, the service will geocode the string and convert it to a latitude/longitude coordinate to calculate directions.
        /// If you pass coordinates, ensure that no space exists between the latitude and longitude values.
        /// </summary>
        public List<string> Destinations { get; set; }

        // 1 Destination:   Lat,Lon
        // 2 Destination:   Lat,Lon
        // 3 Destination:   Town,City,Country

        // destinations=1|2|3&origins=1|2|3


        /// <summary>
        /// sensor — Indicates whether your application is using a sensor (such as a GPS locator) to determine the user's location. 
        /// This value must be either true or false.
        /// </summary>
        public bool Sensor { get; set; }

        #endregion

        #region Optional Parameters

        // TODO: Finish off adding optional request parameters to the request wrapper
        
        #endregion

    }


}
