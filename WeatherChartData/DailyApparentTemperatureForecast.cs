using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiLibrary;
using Normalization;

namespace WeatherChartData
{
    // The DailyApparentTemperatureForecast class containes daily apparent temperature data for chart display
    /// <summary>
    /// The <c>DailyApparentTemperatureForecast</c> class.
    /// Containes daily apparent temperature data for charts.
    /// </summary>
    public class DailyApparentTemperatureForecast
    {
        // The morning apparent temperature list
        /// <value>Gets or sets the morning apparent temperature list.</value>
        public List<TempForecastData> MorningApparentTemperature { get; set; }

        // The day apparent temperature list
        /// <value>Gets or sets the day apparent temperature list.</value>
        public List<TempForecastData> DayApparentTemperature { get; set; }

        // The evening apparent temperature list
        /// <value>Gets or sets the evening apparent temperature list.</value>
        public List<TempForecastData> EveningApparentTemperature { get; set; }

        // The night apparent temperature list
        /// <value>Gets or sets the night apparent temperature list.</value>
        public List<TempForecastData> NightApparentTemperature { get; set; }

        // Copies apparent temperature data from ApiLibrary structures to new structures prepared for chart display
        /// <summary>
        /// Copies apparent temperature data from ApiLibrary structures to new structures prepared for chart display.
        /// </summary>
        /// <param name="dailyForecastSource">DailyWeatherInfoModel object containing apparent day temperature information.</param>
        public DailyApparentTemperatureForecast(DailyWeatherInfoModel dailyForecastSource)
        {
            MorningApparentTemperature = new List<TempForecastData>();
            DayApparentTemperature = new List<TempForecastData>();
            EveningApparentTemperature = new List<TempForecastData>();
            NightApparentTemperature = new List<TempForecastData>();
            int i = 0;
            foreach (var item in dailyForecastSource.Daily)
            {
                string requiredDate = NormalizationOperations.NormalizeDate(dailyForecastSource.Daily[i].Dt);
                MorningApparentTemperature.Add(new TempForecastData(NormalizationOperations.NormalizeTemperature(dailyForecastSource.Daily[i].Feels_like.Morn), requiredDate));
                DayApparentTemperature.Add(new TempForecastData(NormalizationOperations.NormalizeTemperature(dailyForecastSource.Daily[i].Feels_like.Day), requiredDate));
                EveningApparentTemperature.Add(new TempForecastData(NormalizationOperations.NormalizeTemperature(dailyForecastSource.Daily[i].Feels_like.Eve), requiredDate));
                NightApparentTemperature.Add(new TempForecastData(NormalizationOperations.NormalizeTemperature(dailyForecastSource.Daily[i].Feels_like.Night), requiredDate));
                i++;
            }
        }
    }
}
