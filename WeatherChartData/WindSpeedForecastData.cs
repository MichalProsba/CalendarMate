using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChartData
{
    // The WindSpeedForecastData class containes information about wind speed
    /// <summary>
    /// The <c>WindSpeedForecastData</c> class.
    /// Containes information about wind speed.
    /// </summary>
    public class WindSpeedForecastData
    {
        // The wind speed
        /// <value>Gets and sets the wind speed value.</value>
        public double WindSpeed { get; set; }

        // The date
        /// <value>Gets and sets the date value.</value>
        public string Date { get; set; }

        // Copies given parameters to object parameters
        /// <summary>
        /// Copies given parameters to object parameters.
        /// </summary>
        /// <param name="ws">Double wind speed value.</param>
        /// <param name="dt">String date value.</param>
        public WindSpeedForecastData(double ws, string dt)
        {
            WindSpeed = ws;
            Date = dt;
        }
    }
}
