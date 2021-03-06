using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiLibrary;
using Normalization;

namespace WeatherChartData
{
    // The HourlyWeatherForecast class containes hourly weather forecast data for chart display
    /// <summary>
    /// The <c>HourlyWeatherForecast</c> class.
    /// Containes hourly weather forecast data for chart display.
    /// </summary>
    public class HourlyWeatherForecast
    {
        // DateTime object to store the current date and time
        /// <summary>
        /// DateTime object to store the current date and time.
        /// </summary>
        DateTime now;

        // The hourly temperature forecast
        /// <value>Gets and sets the hourly temperature forecast list value.</value>
        public List<TempForecastData> temperature { get; set; }

        // The hourly apparent temperature forecast
        /// <value>Gets and sets the hourly apparent temperature forecast list value.</value>
        public List<TempForecastData> apparentTemperature { get; set; }

        // The hourly humidity forecast
        /// <value>Gets and sets the hourly humidity forecast list value.</value>
        public List<HumidityForecastData> humidity { get; set; }

        // The hourly pressure forecast
        /// <value>Gets and sets the hourly pressure forecast list value.</value>
        public List<PressureForecastData> pressure { get; set; }

        // The hourly wind speed forecast
        /// <value>Gets and sets the hourly wind speed forecast list value.</value>
        public List<WindSpeedForecastData> windSpeed { get; set; }

        // Copies hourly weather forecast data from ApiLibrary structures to new structures prepared for chart display
        /// <summary>
        /// Copies hourly weather forecast data from ApiLibrary structures to new structures prepared for chart display.
        /// </summary>
        /// <param name="dailyForecastSource">HourlyWeatherInfoModel object containing hourly weather forecast information.</param>
        public HourlyWeatherForecast(HourlyWeatherInfoModel dailyForecastSource)
        {
            temperature = new List<TempForecastData>();
            apparentTemperature = new List<TempForecastData>();
            humidity = new List<HumidityForecastData>();
            pressure = new List<PressureForecastData>();
            windSpeed = new List<WindSpeedForecastData>();

            now = DateTime.Now;

            int i = 0;
            foreach (var item in dailyForecastSource.Hourly)
            {
                now = now.AddHours(i);
                string requiredHour = now.Hour.ToString() + ":00";
                temperature.Add(new TempForecastData(NormalizationOperations.NormalizeTemperature(dailyForecastSource.Hourly[i].Temp), requiredHour));
                apparentTemperature.Add(new TempForecastData(NormalizationOperations.NormalizeTemperature(dailyForecastSource.Hourly[i].Feels_like), requiredHour));
                humidity.Add(new HumidityForecastData(dailyForecastSource.Hourly[i].Humidity, requiredHour));
                pressure.Add(new PressureForecastData(dailyForecastSource.Hourly[i].Pressure, requiredHour));
                windSpeed.Add(new WindSpeedForecastData(dailyForecastSource.Hourly[i].Wind_speed, requiredHour));
                i++;
            }
        }
    }
}
