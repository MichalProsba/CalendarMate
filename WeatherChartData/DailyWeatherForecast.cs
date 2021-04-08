using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiLibrary;
using Normalization;

namespace WeatherChartData
{
    public class DailyWeatherForecast
    {
        public DailyTemperatureForecast dailyTemperatureForecast { get; set; }
        public DailyApparentTemperatureForecast dailyApparentTemperatureForecast { get; set; }
        public List<HumidityForecastData> humidity { get; set; }
        public List<PressureForecastData> pressure { get; set; }
        public List<WindSpeedForecastData> windSpeed { get; set; }
        

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
