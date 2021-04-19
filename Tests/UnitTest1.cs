using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
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
    }
}