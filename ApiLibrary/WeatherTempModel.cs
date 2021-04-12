using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The WeatherTempModel class containes temperature information
    /// <summary>
    /// The <c>WeatherTempModel</c> class.
    /// Containes temperature information.
    /// </summary>
    public class WeatherTempModel
    {
        // The day temperature
        /// <value>Gets and sets the day temperature value.</value>
        public double Day { get; set; }

        // The Night temperature
        /// <value>Gets and sets the Night temperature value.</value>
        public double Night { get; set; }

        // The Evening temperature
        /// <value>Gets and sets the Evening temperature value.</value>
        public double Eve { get; set; }

        // The Morning temperature
        /// <value>Gets and sets the Morning temperature value.</value>
        public double Morn { get; set; }
    }
}