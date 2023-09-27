namespace WeatherApi.Service;

public interface IWeatherDataProvider
{
    Task<string> GetCurrentAsync(double lat, double lon);
}