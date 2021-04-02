using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    public class DayWeatherModel
    {
        public double Sunrise { get; set; }
        public double Sunset { get; set; }
        public WeatherTempModel Temp { get; set; }
        public WeatherFeelsLikeModel Feels_like { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public float Wind_speed { get; set; }
        public List<WeatherWeatherModel> Weather { get; set; }
    }
}