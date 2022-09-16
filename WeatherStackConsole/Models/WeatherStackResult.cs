using System.Text.Json.Serialization;

namespace WeatherStackConsole.Models
{
    public class WeatherStackResult
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("error")]
        public WeatherStackError Error { get; set; }
    }
}
