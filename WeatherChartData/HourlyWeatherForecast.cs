using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiLibrary;
using Normalization;

namespace WeatherChartData
{
    public class HourlyWeatherForecast
    {
        public List<TempForecastData> temperature { get; set; }
        public List<TempForecastData> apparentTemperature { get; set; }
        public List<HumidityForecastData> humidity { get; set; }
        public List<PressureForecastData> pressure { get; set; }
        public List<WindSpeedForecastData> windSpeed { get; set; }

        public HourlyWeatherForecast(HourlyWeatherInfoModel dailyForecastSource)
        {
            temperature = new List<TempForecastData>();
            apparentTemperature = new List<TempForecastData>();
            humidity = new List<HumidityForecastData>();
            pressure = new List<PressureForecastData>();
            windSpeed = new List<WindSpeedForecastData>();

            int i = 0;
            foreach (var item in dailyForecastSource.Hourly)
            {
                string requiredDate = NormalizationOperations.NormalizeDate(dailyForecastSource.Hourly[i].Dt);
                temperature.Add(new TempForecastData(dailyForecastSource.Hourly[i].Temp, requiredDate));
                apparentTemperature.Add(new TempForecastData(dailyForecastSource.Hourly[i].Feels_like, requiredDate));
                humidity.Add(new HumidityForecastData(dailyForecastSource.Hourly[i].Humidity, requiredDate));
                pressure.Add(new PressureForecastData(dailyForecastSource.Hourly[i].Pressure, requiredDate));
                windSpeed.Add(new WindSpeedForecastData(dailyForecastSource.Hourly[i].Wind_speed, requiredDate));
                i++;
            }
        }
    }
}
