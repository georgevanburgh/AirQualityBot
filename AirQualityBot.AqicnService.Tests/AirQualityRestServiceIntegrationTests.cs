using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AirQualityBot.AqicnService.Tests
{
    public class Tests
    {
        private IAirQualityService sut;
        [SetUp]
        public void Setup()
        {
            sut = new AirQualityRestService("demo");
        }

        [Test]
        [TestCase("shanghai")]
        public async Task TestKnownGoodCity(string cityName)
        {
            var result = await sut.GetAirQualityForCityAsync(cityName);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.AirQualityIndex, Is.GreaterThan(0));
            Assert.That(result.City.Name.StartsWith(cityName, StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public async Task TestKnownGoodCoordinates()
        {
            var result = await sut.GetAirQualityForLocation(39.019444, 125.738056);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.AirQualityIndex, Is.GreaterThan(0));
            Console.WriteLine(result.City.Name);
        }
    }
}