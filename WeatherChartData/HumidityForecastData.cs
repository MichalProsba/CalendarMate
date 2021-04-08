using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChartData
{
    public class HumidityForecastData
    {
        public int Humidity { get; set; }
        public string Date { get; set; }
        public HumidityForecastData(int hum, string dt)
        {
            Humidity = hum;
            Date = dt;
        }
    }
}
