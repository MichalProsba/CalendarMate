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
    }
}