using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using WeatherStackConsole.HttpClients;
using WeatherStackConsole.Models;

namespace WeatherStackConsole
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: "C:\\WeatherStackConsole\\logs\\log-.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information
                ).CreateLogger();

            using IHost host = CreateHostBuilder(args).Build();

            try
            {
                Log.Information("Application is Starting");
                while (true)
                {
                    Console.WriteLine("Enter Zipcode: ");
                    string zipCode = Console.ReadLine() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(zipCode))
                    {
                        Console.WriteLine("Please enter valid zip code. (example: 01226)");
                    }

                    var weatherStackClient = host.Services.GetRequiredService<IWeatherStackHttpClient>();
                    var weather = await weatherStackClient.GetWeatherDetails(zipCode);
                    if (weather != null)
                    {
                        Console.WriteLine(Decision.ShouldIGoOutside(weather));
                        Console.WriteLine(Decision.ShouldIWearSunScreen(weather));
                        Console.WriteLine(Decision.CanIFlyKite(weather));
                    }
                    else
                    {
                        Console.WriteLine("Weather Current is null");
                        Log.Information("Weather Current is null");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An error occurred.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureServices((_, services) =>
            {
                services.AddHttpClient<IWeatherStackHttpClient, WeatherStackHttpClient>();
            });

    }
}
