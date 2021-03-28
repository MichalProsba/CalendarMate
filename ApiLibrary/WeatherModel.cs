using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    public class WeatherModel
    {
        public float Temp { get; set; }
        public float Feels_like { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }
}
