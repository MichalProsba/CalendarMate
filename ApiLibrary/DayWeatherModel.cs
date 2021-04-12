using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The DayWeatherModel containes day weather information
    /// <summary>
    /// The <c>DayWeatherModel</c> class.
    /// Containes all variables and methods necessary to store the day weather information that we download from the Weather API (openweathermap.org).
    /// </summary>
    public class DayWeatherModel
    {
        // The time in seconds from 1.1.1970
        /// <value>Gets and sets the time in seconds from 1.1.1970 value.</value>
        public double Dt { get; set; }

        // The sunrise in seconds from 1.1.1970
        /// <value>Gets and sets the sunrise in seconds from 1.1.1970 value.</value>
        public double Sunrise { get; set; }

        // The sunset in seconds from 1.1.1970
        /// <value>Gets and sets the sunset in seconds from 1.1.1970 value.</value>
        public double Sunset { get; set; }

        // The temperature information
        /// <value>Gets and sets the temperature information value.</value>
        public WeatherTempModel Temp { get; set; }

        // The apparent temperature information
        /// <value>Gets and sets the apparent temperature information value.</value>
        public WeatherFeelsLikeModel Feels_like { get; set; }

        // The pressure
        /// <value>Gets and sets the pressure value.</value>
        public int Pressure { get; set; }

        // The humidity
        /// <value>Gets and sets the humidity value.</value>
        public int Humidity { get; set; }

        // The wind speed
        /// <value>Gets and sets the wind speed value.</value>
        public float Wind_speed { get; set; }

        // The Weather information list
        /// <value>Gets and sets the Weather informarmation list value.</value>
        public List<WeatherWeatherModel> Weather { get; set; }
    }
}