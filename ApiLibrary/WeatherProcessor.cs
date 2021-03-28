using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    public static class WeatherProcessor
    {
        public static string city = "Opole";
        public static string country = "pl";
        public static async Task<WeatherModel> LoadCurrentWeather()
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={ city },{ country }&APPID=f75180affde9785ae42c8b8dad08cbd0";

            using(HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    WeatherMainModel weather = await response.Content.ReadAsAsync<WeatherMainModel>();
                    return weather.Main;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
