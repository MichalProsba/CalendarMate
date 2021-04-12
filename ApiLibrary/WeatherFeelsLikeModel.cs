using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The WeatherTempModel class containes apparent temperature information
    /// <summary>
    /// The <c>WeatherTempModel</c> class.
    /// Containes apparent temperature information.
    /// </summary>
    public class WeatherFeelsLikeModel
    {
        // The day apparent temperature
        /// <value>Gets and sets the day apparent temperature value.</value>
        public double Day { get; set; }

        // The Night apparent temperature
        /// <value>Gets and sets the Night apparent temperature value.</value>
        public double Night { get; set; }

        // The Evening apparent temperature
        /// <value>Gets and sets the Evening apparent temperature value.</value>
        public double Eve { get; set; }

        // The Morning apparent temperature
        /// <value>Gets and sets the Morning apparent temperature value.</value>
        public double Morn { get; set; }
    }
}