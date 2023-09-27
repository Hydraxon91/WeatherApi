using System.Net;
using System.Text.Json;

namespace WeatherApi.Service;

public class OpenWeatherMapApi : IWeatherDataProvider
{
    private readonly ILogger<OpenWeatherMapApi> _logger;
    
    public OpenWeatherMapApi(ILogger<OpenWeatherMapApi> logger)
    {
        _logger = logger;
    }
    
    public async Task<string> GetCurrentAsync(double lat, double lon)
    {
        var apiKey = "69883ee2fa7a4b6cec1f2856c0622922";
        var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";

        using var client = new HttpClient();
        _logger.LogInformation("Calling OpenWeather API with url: {url}", url);

        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
    
}