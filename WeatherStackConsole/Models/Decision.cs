using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStackConsole.Models
{
    public class Decision
    {
        public static string ShouldIGoOutside(WeatherStackResponse weather)
        {
            if (weather.Current.WeatherDescriptions.Any(desc => desc.ToLower().Contains("rain")))
            {
                return "Should I go outside? No";
            }
            else
            {
                return "Should I go outside? Yes";
            }
        }

        public static string ShouldIWearSunScreen(WeatherStackResponse weather)
        {
            if (weather.Current.UvIndex > 3)
            {
                return "Should I wear sunscreen? Yes";
            }
            else
            {
                return "Should I wear sunscreen? No";
            }
        }
        public static string CanIFlyKite(WeatherStackResponse weather)
        {
            if (!weather.Current.WeatherDescriptions.Contains("rain")
                && weather.Current.WindSpeed > 15)
            {
                return "Can I fly my kite? Yes";
            }
            else
            {
                return "Can I fly my kite? No";
            }
        }
    }
}
