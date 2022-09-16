using WeatherStackConsole.Models;

namespace WeatherStackConsole.HttpClients
{
    public interface IWeatherStackHttpClient
    {
        Task<WeatherStackResponse> GetWeatherDetails(string zipCode);
    }
}
