using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChartData
{
    // The HumidityForecastData class containes information about humidity
    /// <summary>
    /// The <c>HumidityForecastData</c> class.
    /// Containes information about humidity.
    /// </summary>
    public class HumidityForecastData
    {
        // The humidity
        /// <value>Gets and sets the humidity value.</value>
        public int Humidity { get; set; }

        // The date
        /// <value>Gets and sets the date value.</value>
        public string Date { get; set; }

        // Copies given parameters to object parameters
        /// <summary>
        /// Copies given parameters to object parameters.
        /// </summary>
        /// <param name="hum">Double humidity value.</param>
        /// <param name="dt">String date value.</param>
        public HumidityForecastData(int hum, string dt)
        {
            Humidity = hum;
            Date = dt;
        }
    }
}
