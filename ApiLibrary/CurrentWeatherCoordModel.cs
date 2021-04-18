using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The CurrentWeatherCoordModel class containes geographical coordinates
    /// <summary>
    /// The <c>CurrentWeatherCoordModel</c> class.
    /// Containes geographical coordinates.
    /// </summary>
    public class CurrentWeatherCoordModel
    {
        // The longitude
        /// <value>Gets and sets the Longitude value.</value>
        public string Lon { get; set; }

        // The Latitude
        /// <value>Gets and sets the Latitude value.</value>
        public string Lat { get; set; }
    }
}