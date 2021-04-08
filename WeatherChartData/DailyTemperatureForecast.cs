using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiLibrary;
using Normalization;

namespace WeatherChartData
{
    public class DailyTemperatureForecast
    {
        public List<TempForecastData> MorningTemperature { get; set; }
        public List<TempForecastData> DayTemperature { get; set; }
        public List<TempForecastData> EveningTemperature { get; set; }
        public List<TempForecastData> NightTemperature { get; set; }

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
