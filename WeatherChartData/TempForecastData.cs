using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChartData
{
    // The TempForecastData class containes information about temperature
    /// <summary>
    /// The <c>TempForecastData</c> class.
    /// Containes information about temperature.
    /// </summary>
    public class TempForecastData
    {
        // The temperature
        /// <value>Gets and sets the temperature value.</value>
        public double Temperature { get; set; }

        // The date
        /// <value>Gets and sets the date value.</value>
        public string Date { get; set; }

        // Copies given parameters to object parameters
        /// <summary>
        /// Copies given parameters to object parameters.
        /// </summary>
        /// <param name="temp">Double temperature value.</param>
        /// <param name="dt">String date value.</param>
        public TempForecastData(double temp, string dt)
        {
            Temperature = temp;
            Date = dt;
        }
    }
}
