using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // The DailyWeatherInfoProcessor class enables downloading daily weather forecast information
    /// <summary>
    /// The <c>DailyWeatherInfoProcessor</c> class.
    /// Containes variables and methods necessary to download daily weather forecast information from weather API (openweathermap.org).
    /// </summary>
    public static class DailyWeatherInfoProcessor
    {
        // The longitude
        /// <value>Containes the Longitude value.</value>
        public static string lat = "50.6667";

        // The Latitude
        /// <value>Containes the Latitude value.</value>
        public static string lon = "17.95";

        // Download and process daily weather forecast information
        /// <summary>
        /// Download and process daily weather forecast information.
        /// </summary>
        /// <returns>Daily weather forecast information inside an DailyWeatherInfoModel class object or a exception response in an Exception class object.</returns>
        public static async Task<DailyWeatherInfoModel> LoadDailyWeather()
        {
            string url = $"https://api.openweathermap.org/data/2.5/onecall?lat={ lat }&lon={ lon }&exclude=current,minutely,hourly,alerts&appid=f75180affde9785ae42c8b8dad08cbd0";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    DailyWeatherInfoModel dailyWeather = await response.Content.ReadAsAsync<DailyWeatherInfoModel>();
                    return dailyWeather;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
