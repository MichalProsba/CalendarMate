using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChartData
{
    public class WindSpeedForecastData
    {
        public double WindSpeed { get; set; }
        public string Date { get; set; }
        public WindSpeedForecastData(double ws, string dt)
        {
            WindSpeed = ws;
            Date = dt;
        }
    }
}
