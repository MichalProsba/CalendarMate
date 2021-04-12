using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The HourlyWeatherInfoModel class containes information about 48-hour weather forecast
    /// <summary>
    /// The <c>HourlyWeatherInfoModel</c> class.
    /// Containes information about 48-hour weather forecast.
    /// </summary>
    public class HourlyWeatherInfoModel
    {
        // The hourly weather forecast information list
        /// <value>Gets and sets the hourly weather forecast list value.</value>
        public List<HourWeatherModel> Hourly { get; set; }
    }
}
