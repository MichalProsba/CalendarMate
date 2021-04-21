using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The CurrentWeatherInfoModel class containes current weather information
    /// <summary>
    /// The <c>CurrentWeatherInfoModel</c> class.
    /// Containes all variables and methods necessary to store the current weather information that we download from the Weather API (openweathermap.org).
    /// </summary>
    public class CurrentWeatherInfoModel
    {
        // The Coordinates object
        /// <value>Gets and sets the Coordinates value.</value>
        public CurrentWeatherCoordModel Coord { get; set; }

        // The Weather information list
        /// <value>Gets and sets the Weather informarmation list value.</value>
        public List<WeatherWeatherModel> Weather { get; set; }

        // The Weather Main information
        /// <value>Gets and sets the Weather Main information value.</value>
        public CurrentWeatherMainModel Main { get; set; }

        // The Wind information
        /// <value>Gets and sets the Wind information value.</value>
        public CurrentWeatherWindModel Wind { get; set; }

        // The Sun information
        /// <value>Gets and sets the Sun information value.</value>
        public CurrentWeatherSysModel Sys { get; set; }

        // The Terytory Name
        /// <value>Gets and sets the Terytory Name value.</value>
        public string Name { get; set; }

        // The Visibility
        /// <value>Gets and sets the Visibility value.</value>
        public string Visibility { get; set; }

        // The Timezone
        /// <value>Gets and sets the Timezone value.</value>
        public double Timezone { get; set; }

        // The Cod
        /// <value>Gets and sets the Cod value.</value>
        public string Cod { get; set; }

        // Sets Cod value to "404"
        /// <summary>
        /// Sets Cod value to "404" 
        /// </summary>
        public CurrentWeatherInfoModel()
        {
            Cod = "404";
        }
    }
}
