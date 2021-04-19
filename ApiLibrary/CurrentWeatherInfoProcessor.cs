using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary 
{
    // The CurrentWeatherInfoProcessor class enables downloading current weather information
    /// <summary>
    /// The <c>CurrentWeatherInfoProcessor</c> class.
    /// Containes variables and methods necessary to download current weather information from weather API (openweathermap.org).
    /// </summary>
    public static class CurrentWeatherInfoProcessor
    {
        // Download and process current weather information
        /// <summary>
        /// Download and process current weather information.
        /// </summary>
        /// <returns>Weather information inside an CurrentWeatherInfoModel class object or a exception response in an Exception class object.</returns>
        public static async Task<CurrentWeatherInfoModel> LoadCurrentWeather(string city)
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
                    CurrentWeatherInfoModel currentWeather = new CurrentWeatherInfoModel();
                    return currentWeather;
                }
            }
        }
    }
}
