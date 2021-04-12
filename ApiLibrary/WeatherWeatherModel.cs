using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The WeatherWeatherModel class containes basic information about the weather
    /// <summary>
    /// The <c>WeatherWeatherModel</c> class.
    /// Containes basic information about the weather.
    /// </summary>
    public class WeatherWeatherModel
    {
        // The weather icon name
        /// <value>Gets and sets the weather icon name value.</value>
        public string Icon { get; set; }

        // The weather description
        /// <value>Gets and sets the weather description value.</value>
        public string Description { get; set; }
    }
}
