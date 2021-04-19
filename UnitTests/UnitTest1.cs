using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class Tests
    {
        private const string Expected = "Opole";
        [SetUp]
        public void Setup()
        {
        }

        [TestCase]
        public void ReturnCity()
        {
            
            Assert.Pass();
        }
    }
}