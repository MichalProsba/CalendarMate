using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiLibrary;
using Normalization;

namespace WeatherChartData
{
    // The DailyWeatherForecast class containes daily weather forecast data for chart display
    /// <summary>
    /// The <c>DailyWeatherForecast</c> class.
    /// Containes daily weather forecast data for chart display.
    /// </summary>
    public class DailyWeatherForecast
    {
        // The daily temperature forecast
        /// <value>Gets and sets the daily temperature forecast value.</value>
        public DailyTemperatureForecast dailyTemperatureForecast { get; set; }

        // The daily apparent temperature forecast
        /// <value>Gets and sets the daily apparent temperature forecast value.</value>
        public DailyApparentTemperatureForecast dailyApparentTemperatureForecast { get; set; }

        // The daily humidity forecast
        /// <value>Gets and sets the daily humidity forecast value.</value>
        public List<HumidityForecastData> humidity { get; set; }

        // The daily pressure forecast
        /// <value>Gets and sets the daily pressure forecast value.</value>
        public List<PressureForecastData> pressure { get; set; }

        // The daily wind speed forecast
        /// <value>Gets and sets the daily wind speed forecast value.</value>
        public List<WindSpeedForecastData> windSpeed { get; set; }

        // Copies daily weather forecast data from ApiLibrary structures to new structures prepared for chart display
        /// <summary>
        /// Copies daily weather forecast data from ApiLibrary structures to new structures prepared for chart display.
        /// </summary>
        /// <param name="dailyForecastSource">DailyWeatherInfoModel object containing daily weather forecast information.</param>
        public DailyWeatherForecast(DailyWeatherInfoModel dailyForecastSource) 
        {
            dailyTemperatureForecast = new DailyTemperatureForecast(dailyForecastSource);
            dailyApparentTemperatureForecast = new DailyApparentTemperatureForecast(dailyForecastSource);

            humidity = new List<HumidityForecastData>();
            pressure = new List<PressureForecastData>();
            windSpeed = new List<WindSpeedForecastData>();

            int i = 0;
            foreach (var item in dailyForecastSource.Daily)
            {
                string requiredDate = NormalizationOperations.NormalizeDate(dailyForecastSource.Daily[i].Dt);
                humidity.Add(new HumidityForecastData(dailyForecastSource.Daily[i].Humidity, requiredDate));
                pressure.Add(new PressureForecastData(dailyForecastSource.Daily[i].Pressure, requiredDate));
                windSpeed.Add(new WindSpeedForecastData(dailyForecastSource.Daily[i].Wind_speed, requiredDate));
                i++;
            }
        }
    }
}
