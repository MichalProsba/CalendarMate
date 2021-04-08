using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiLibrary;
using Normalization;

namespace WeatherChartData
{
    public class DailyApparentTemperatureForecast
    {
        public List<TempForecastData> MorningApparentTemperature { get; set; }
        public List<TempForecastData> DayApparentTemperature { get; set; }
        public List<TempForecastData> EveningApparentTemperature { get; set; }
        public List<TempForecastData> NightApparentTemperature { get; set; }

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
