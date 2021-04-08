using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChartData
{
    public class TempForecastData
    {
        public double Temperature { get; set; }
        public string Date { get; set; }
        public TempForecastData(double temp, string dt)
        {
            Temperature = temp;
            Date = dt;
        }
    }
}
