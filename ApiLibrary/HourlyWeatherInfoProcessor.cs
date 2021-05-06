using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The HourlyWeatherInfoProcessor class enables downloading hourly weather forecast information
    /// <summary>
    /// The <c>HourlyWeatherInfoProcessor</c> class.
    /// Containes variables and methods necessary to download hourly weather forecast information from weather API (openweathermap.org).
    /// </summary>
    public static class HourlyWeatherInfoProcessor
    {
        // Download and process hourly weather forecast information
        /// <summary>
        /// Download and process hourly weather forecast information.
        /// </summary>
        /// <returns>Hourly weather forecast information inside an HourlyWeatherInfoModel class object or a exception response in an Exception class object.</returns>
        public static async Task<HourlyWeatherInfoModel> LoadHourlyWeather(string lon, string lat)
        {
            string url = $"https://api.openweathermap.org/data/2.5/onecall?lat={ lat }&lon={ lon }&exclude=current,minutely,daily,alerts&appid=f75180affde9785ae42c8b8dad08cbd0";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    HourlyWeatherInfoModel hourlyWeather = await response.Content.ReadAsAsync<HourlyWeatherInfoModel>();
                    return hourlyWeather;
                }
                else
                {
                    throw new Exception("API doesn't respond.");
                }
            }
        }
    }
}
