using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    public class CurrentWeatherInfoModel
    {
        public CurrentWeatherCoordModel Coord { get; set; }
        public List<WeatherWeatherModel> Weather { get; set; }
        public CurrentWeatherMainModel Main { get; set; }
        public CurrentWeatherWindModel Wind { get; set; }
        public CurrentWeatherSysModel Sys { get; set; }
        public string Name { get; set; }
        public string Visibility { get; set; }
        public string Timezone { get; set; }
    }
}
