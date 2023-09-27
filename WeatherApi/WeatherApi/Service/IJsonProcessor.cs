namespace WeatherApi.Service;

public interface IJsonProcessor
{
    WeatherForecast Process(string data);
}