using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChartData
{
    // The PressureForecastData class containes information about pressure
    /// <summary>
    /// The <c>PressureForecastData</c> class.
    /// Containes information about pressure.
    /// </summary>
    public class PressureForecastData
    {
        // The pressure
        /// <value>Gets and sets the pressure value.</value>
        public int Pressure { get; set; }

        // The date
        /// <value>Gets and sets the date value.</value>
        public string Date { get; set; }

        // Copies given parameters to object parameters
        /// <summary>
        /// Copies given parameters to object parameters.
        /// </summary>
        /// <param name="pre">Double pressure value.</param>
        /// <param name="dt">String date value.</param>
        public PressureForecastData(int pre, string dt)
        {
            Pressure = pre;
            Date = dt;
        }
    }
}
