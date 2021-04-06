using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary 
{
    public static class CurrentWeatherInfoProcessor
    {
        public static string city = "Opole";
        public static async Task<CurrentWeatherInfoModel> LoadCurrentWeather()
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={ city }&APPID=f75180affde9785ae42c8b8dad08cbd0";

            using(HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    CurrentWeatherInfoModel currentWeather = await response.Content.ReadAsAsync<CurrentWeatherInfoModel>();
                    return currentWeather;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
