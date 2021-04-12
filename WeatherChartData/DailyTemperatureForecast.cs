using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiLibrary;
using Normalization;

namespace WeatherChartData
{
    // The DailyApparentTemperatureForecast class containes daily temperature data for chart display
    /// <summary>
    /// The <c>DailyApparentTemperatureForecast</c> class.
    /// Containes daily temperature data for charts.
    /// </summary>
    public class DailyTemperatureForecast
    {
        // The morning temperature list
        /// <value>Gets or sets the morning temperature list.</value>
        public List<TempForecastData> MorningTemperature { get; set; }

        // The day temperature list
        /// <value>Gets or sets the day temperature list.</value>
        public List<TempForecastData> DayTemperature { get; set; }

        // The evening temperature list
        /// <value>Gets or sets the evening temperature list.</value>
        public List<TempForecastData> EveningTemperature { get; set; }

        // The night temperature list
        /// <value>Gets or sets the night temperature list.</value>
        public List<TempForecastData> NightTemperature { get; set; }

        // Copies temperature data from ApiLibrary structures to new structures prepared for chart display
        /// <summary>
        /// Copies temperature data from ApiLibrary structures to new structures prepared for chart display.
        /// </summary>
        /// <param name="dailyForecastSource">DailyWeatherInfoModel object containing day temperature information.</param>
        public DailyTemperatureForecast(DailyWeatherInfoModel dailyForecastSource)
        {
            MorningTemperature = new List<TempForecastData>();
            DayTemperature = new List<TempForecastData>();
            EveningTemperature = new List<TempForecastData>();
            NightTemperature = new List<TempForecastData>();
            int i = 0;
            foreach (var item in dailyForecastSource.Daily)
            {
                string requiredDate = NormalizationOperations.NormalizeDate(dailyForecastSource.Daily[i].Dt);
                MorningTemperature.Add(new TempForecastData(NormalizationOperations.NormalizeTemperature(dailyForecastSource.Daily[i].Temp.Morn), requiredDate));
                DayTemperature.Add(new TempForecastData(NormalizationOperations.NormalizeTemperature(dailyForecastSource.Daily[i].Temp.Day), requiredDate));
                EveningTemperature.Add(new TempForecastData(NormalizationOperations.NormalizeTemperature(dailyForecastSource.Daily[i].Temp.Eve), requiredDate));
                NightTemperature.Add(new TempForecastData(NormalizationOperations.NormalizeTemperature(dailyForecastSource.Daily[i].Temp.Night), requiredDate));
                i++;
            }
        }
    }
}
