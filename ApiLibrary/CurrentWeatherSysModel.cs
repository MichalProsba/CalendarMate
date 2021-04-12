using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The CurrentWeatherSysModel class containes information abut sunrise and sunset
    /// <summary>
    /// The <c>CurrentWeatherSysModel</c> class.
    /// Containes information abut sunrise and sunset.
    /// </summary>
    public class CurrentWeatherSysModel
    {
        // The Sunrise Time
        /// <value>Gets and sets the sunrise time value.</value>
        public double Sunrise { get; set; }

        // The Sunset Time
        /// <value>Gets and sets the sunset time value.</value>
        public double Sunset { get; set; }
    }
}
