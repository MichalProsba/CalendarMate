using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The CurrentWeatherMainModel class containes basic weather information
    /// <summary>
    /// The <c>CurrentWeatherMainModel</c> class.
    /// Containes basic weather information. 
    /// </summary>
    public class CurrentWeatherMainModel
    {
        // The Temperature
        /// <value>Gets and sets the temperature value.</value>
        public float Temp { get; set; }

        // The Apparent Temperature
        /// <value>Gets and sets the apparent temperature value.</value>
        public float Feels_like { get; set; }

        // The Pressure
        /// <value>Gets and sets the pressure value.</value>
        public int Pressure { get; set; }

        // The Humidity
        /// <value>Gets and sets the humidity value.</value>
        public int Humidity { get; set; }
    }
}
