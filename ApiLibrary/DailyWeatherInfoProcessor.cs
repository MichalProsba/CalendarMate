using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    public static class DailyWeatherInfoProcessor
    {
        public static string lat = "50.6667";
        public static string lon = "17.95";
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
