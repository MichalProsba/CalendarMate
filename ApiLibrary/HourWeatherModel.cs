using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The HourWeatherModel containes hour weather information
    /// <summary>
    /// The <c>HourWeatherModel</c> class.
    /// Containes all variables and methods necessary to store the hour weather information that we download from the Weather API (openweathermap.org).
    /// </summary>
    public class HourWeatherModel
    {
        // The time in seconds from 1.1.1970
        /// <value>Gets and sets the time in seconds from 1.1.1970 value.</value>
        public double Dt { get; set; }

        // The temperature
        /// <value>Gets and sets the temperature value.</value>
        public float Temp { get; set; }

        // The apparent temperature
        /// <value>Gets and sets the apparent temperature value.</value>
        public float Feels_like { get; set; }

        // The pressure
        /// <value>Gets and sets the pressure value.</value>
        public int Pressure { get; set; }

        // The humidity
        /// <value>Gets and sets the humidity value.</value>
        public int Humidity { get; set; }

        // The Visibility
        /// <value>Gets and sets the Visibility value.</value>
        public int Visibility { get; set; }

        // The wind speed
        /// <value>Gets and sets the wind speed value.</value>
        public float Wind_speed { get; set; }

        // The Weather information list
        /// <value>Gets and sets the Weather informarmation list value.</value>
        public List<WeatherWeatherModel> Weather { get; set; }
    }
}