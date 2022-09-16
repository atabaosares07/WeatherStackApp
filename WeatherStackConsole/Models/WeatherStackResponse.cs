namespace WeatherStackConsole.Models
{
    public class WeatherStackResponse
    {
        public WeatherStackRequest Request { get; set; }
        public Location Location { get; set; }
        public Current Current { get; set; }
    }
}
