using Microsoft.Extensions.Configuration;
using Serilog;
using System.Net;
using System.Text.Json;
using WeatherStackConsole.Models;

namespace WeatherStackConsole.HttpClients
{
    public class WeatherStackHttpClient : IWeatherStackHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly JsonSerializerOptions _options;

        public WeatherStackHttpClient(HttpClient httpClient, IConfiguration config)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _httpClient = httpClient;
            _config = config;
            _httpClient.BaseAddress = new Uri("http://api.weatherstack.com");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<WeatherStackResponse> GetWeatherDetails(string zipCode)
        {
            WeatherStackResponse? weatherStackResponse = null;
            try
            {
                var response = await _httpClient.GetAsync($"current?access_key={this.GetKey()}&query={zipCode}");
                response.EnsureSuccessStatusCode();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    Log.Information(responseString);
                    WeatherStackResult weatherStackResult = JsonSerializer.Deserialize<WeatherStackResult>(responseString, _options);
                    if (weatherStackResult.Error == null)
                    {
                        weatherStackResponse = JsonSerializer.Deserialize<WeatherStackResponse>(responseString, _options);
                    }
                    else
                    {
                        Log.Error($"{weatherStackResult.Error.Type} (Error Code: {weatherStackResult.Error.Code})");
                        Log.Error($"{weatherStackResult.Error.Info}");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Fatal($"An error occurred: {ex.Message}");
            }

            return weatherStackResponse;
        }

        private string GetKey()
        {
            return _config.GetValue<string>("WeatherStackAccessKey");
        }
    }
}
