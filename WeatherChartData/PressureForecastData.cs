using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChartData
{
    public class PressureForecastData
    {
        public int Pressure { get; set; }
        public string Date { get; set; }
        public PressureForecastData(int pre, string dt)
        {
            Pressure = pre;
            Date = dt;
        }
    }
}
