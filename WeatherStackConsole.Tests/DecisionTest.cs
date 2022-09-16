using WeatherStackConsole.Models;

namespace WeatherStackConsole.Tests
{
    [TestClass]
    public class DecisionTest
    {
        [TestMethod]
        public void ShouldIGoOutside_Raining_Returns_No()
        {
            WeatherStackResponse weather = new WeatherStackResponse();
            weather.Current = new Current();
            weather.Current.WeatherDescriptions = new List<string>();
            weather.Current.WeatherDescriptions.Add("Light Rain");
            
            var actual = Decision.ShouldIGoOutside(weather);

            Assert.AreEqual("Should I go outside? No", actual);
        }

        [TestMethod]
        public void ShouldIGoOutside_NotRaining_Returns_Yes()
        {
            WeatherStackResponse weather = new WeatherStackResponse();
            weather.Current = new Current();
            weather.Current.WeatherDescriptions = new List<string>();
            weather.Current.WeatherDescriptions.Add("Sunny");

            var actual = Decision.ShouldIGoOutside(weather);

            Assert.AreEqual("Should I go outside? Yes", actual);
        }

        [TestMethod]
        public void ShouldIWearSunScreen_UvIndexGreaterThan3_Returns_Yes()
        {
            WeatherStackResponse weather = new WeatherStackResponse();
            weather.Current = new Current();
            weather.Current.UvIndex = 5;

            var actual = Decision.ShouldIWearSunScreen(weather);

            Assert.AreEqual("Should I wear sunscreen? Yes", actual);
        }

        [TestMethod]
        public void ShouldIWearSunScreen_UvIndexLessThanOrEqual3_Returns_No()
        {
            WeatherStackResponse weather = new WeatherStackResponse();
            weather.Current = new Current();
            weather.Current.UvIndex = 3;

            var actual = Decision.ShouldIWearSunScreen(weather);

            Assert.AreEqual("Should I wear sunscreen? No", actual);
        }

        [TestMethod]
        public void CanIFlyKite_Raining_And_WindSpeedGreaterthan15_Returns_Yes()
        {
            WeatherStackResponse weather = new WeatherStackResponse();
            weather.Current = new Current();
            weather.Current.WeatherDescriptions = new List<string>();
            weather.Current.WeatherDescriptions.Add("Light Rain");
            weather.Current.WindSpeed = 16;

            var actual = Decision.CanIFlyKite(weather);

            Assert.AreEqual("Can I fly my kite? Yes", actual);
        }

        [TestMethod]
        public void CanIFlyKite_NotRaining_And_WindSpeedGreaterthan15_Returns_No()
        {
            WeatherStackResponse weather = new WeatherStackResponse();
            weather.Current = new Current();
            weather.Current.WeatherDescriptions = new List<string>();
            weather.Current.WeatherDescriptions.Add("Sunny");
            weather.Current.WindSpeed = 16;

            var actual = Decision.CanIFlyKite(weather);

            Assert.AreEqual("Can I fly my kite? Yes", actual);
        }

        [TestMethod]
        public void CanIFlyKite_Raining_And_WindSpeedLessthanOrEqual15_Returns_No()
        {
            WeatherStackResponse weather = new WeatherStackResponse();
            weather.Current = new Current();
            weather.Current.WeatherDescriptions = new List<string>();
            weather.Current.WeatherDescriptions.Add("Light Rain");
            weather.Current.WindSpeed = 15;

            var actual = Decision.CanIFlyKite(weather);

            Assert.AreEqual("Can I fly my kite? No", actual);
        }

        [TestMethod]
        public void CanIFlyKite_NotRaining_And_WindSpeedLessthanOrEqual15_Returns_No()
        {
            WeatherStackResponse weather = new WeatherStackResponse();
            weather.Current = new Current();
            weather.Current.WeatherDescriptions = new List<string>();
            weather.Current.WeatherDescriptions.Add("Sunny");
            weather.Current.WindSpeed = 15;

            var actual = Decision.CanIFlyKite(weather);

            Assert.AreEqual("Can I fly my kite? No", actual);
        }
    }
}