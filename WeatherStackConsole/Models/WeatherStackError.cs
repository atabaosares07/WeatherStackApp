using System.Text.Json.Serialization;

namespace WeatherStackConsole.Models
{
    public class WeatherStackError
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("info")]
        public string Info { get; set; }
    }
}
