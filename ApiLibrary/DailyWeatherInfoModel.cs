using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The DailyWeatherInfoModel class containes information about 7-day weather forecast
    /// <summary>
    /// The <c>DailyWeatherInfoModel</c> class.
    /// Containes information about 7-day weather forecast.
    /// </summary>
    public class DailyWeatherInfoModel
    {
        // The daily weather forecast information list
        /// <value>Gets and sets the daily weather forecast list value.</value>
        public List<DayWeatherModel> Daily { get; set; }
    }
}
