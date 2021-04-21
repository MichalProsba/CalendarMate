using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        // //////////////////////////////
        // Normalization project tests //
        // //////////////////////////////
        [Test]
        public void NormalizeTemperatureTest()
        {
            double result = Normalization.NormalizationOperations.NormalizeTemperature(273.15);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void NormalizeDateTest()
        {
            string result = Normalization.NormalizationOperations.NormalizeDate(86400);
            Assert.AreEqual("02.01.1970", result);
        }

        // ///////////////////////////
        // ApiLibrary project tests //
        // ///////////////////////////
        [Test]
        public void InitializeClientTest()
        {
            ApiLibrary.ApiHelper.InitializeClient();
            Assert.IsNotNull(ApiLibrary.ApiHelper.ApiClient);
        }
        
        [Test]
        public void CurrentWeatherInfoModelTest()
        {
            ApiLibrary.CurrentWeatherInfoModel obj = new ApiLibrary.CurrentWeatherInfoModel();
            Assert.IsTrue(obj.Cod == "404");
        }

        [Test]
        public void DailyWeatherInfoModelTest()
        {
            ApiLibrary.DailyWeatherInfoModel obj = new ApiLibrary.DailyWeatherInfoModel();
            Assert.IsNotNull(obj);
        }

        [Test]
        public void HourlyWeatherInfoModelTest()
        {
            ApiLibrary.HourlyWeatherInfoModel obj = new ApiLibrary.HourlyWeatherInfoModel();
            Assert.IsNotNull(obj);
        }

        // /////////////////////////////////
        // WeatherChartData project tests //
        // /////////////////////////////////
        [Test]
        public void WindSpeedForecastDataTest()
        {
            WeatherChartData.WindSpeedForecastData obj = new WeatherChartData.WindSpeedForecastData(10.12, "10.12.2021");
            Assert.IsTrue(obj.WindSpeed == 10.12 && obj.Date == "10.12.2021");
        }

        [Test]
        public void HumidityForecastDataTest()
        {
            WeatherChartData.HumidityForecastData obj = new WeatherChartData.HumidityForecastData(74, "10.12.2021");
            Assert.IsTrue(obj.Humidity == 74 && obj.Date == "10.12.2021");
        }

        [Test]
        public void PressureForecastDataTest()
        {
            WeatherChartData.PressureForecastData obj = new WeatherChartData.PressureForecastData(998, "10.12.2021");
            Assert.IsTrue(obj.Pressure == 998 && obj.Date == "10.12.2021");
        }

        [Test]
        public void TempForecastDataTest()
        {
            WeatherChartData.TempForecastData obj = new WeatherChartData.TempForecastData(35.17, "10.12.2021");
            Assert.IsTrue(obj.Temperature == 35.17 && obj.Date == "10.12.2021");
        }
    }
}