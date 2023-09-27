using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Service;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherDataProvider _weatherDataProvider;
    private readonly IJsonProcessor _jsonProcessor;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherDataProvider weatherDataProvider,
        IJsonProcessor jsonProcessor)
    {
        _logger = logger;
        _weatherDataProvider = weatherDataProvider;
        _jsonProcessor = jsonProcessor;
    }

    // [HttpGet("GetWeatherForecast")]
    // public IEnumerable<WeatherForecast> Get()
    // {
    //     Console.WriteLine("Get request accepted");
    //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //         {
    //             Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //             TemperatureC = Random.Shared.Next(-20, 55),
    //             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //         })
    //         .ToArray();
    // }

    // [HttpGet("Get")]
    // public IEnumerable<WeatherForecast> Get(DateTime date)
    // {
    //     if (date.Year < 2023)
    //     {
    //         throw new ArgumentOutOfRangeException(nameof(date));
    //     }
    //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //         {
    //             Date = DateOnly.FromDateTime(date.AddDays(index)),
    //             TemperatureC = Random.Shared.Next(-20, 55),
    //             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //         })
    //         .ToArray();
    // }
    
    // [HttpGet("Get")]
    // public IActionResult Get(DateTime date)
    // {
    //     if (date.Year < 2023)
    //     {
    //         return NotFound("Invalid date. Please provide a date before 2023.");
    //     }
    //
    //     var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //         {
    //             Date = DateOnly.FromDateTime(date.AddDays(index)),
    //             TemperatureC = Random.Shared.Next(-20, 55),
    //             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //         })
    //         .ToArray();
    //
    //     return Ok(forecasts);
    // }
    
    // [HttpGet("Get")]
    // public ActionResult<IEnumerable<WeatherForecast>> Get(DateTime date)
    // {
    //     if (date.Year < 2023)
    //     {
    //         return NotFound("Invalid date. Please provide a date before 2023.");
    //     }
    //
    //     var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //         {
    //             Date = DateOnly.FromDateTime(date.AddDays(index)),
    //             TemperatureC = Random.Shared.Next(-20, 55),
    //             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //         })
    //         .ToArray();
    //
    //     return Ok(forecasts);
    // }
    
    // [HttpGet("GetCity")]
    // public IEnumerable<WeatherForecast> Get([Required] DateTime date, [Required] string city)
    // {
    //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //         {
    //             Date = DateOnly.FromDateTime(date.AddDays(index)),
    //             TemperatureC = Random.Shared.Next(-20, 55),
    //             Summary = Summaries[Random.Shared.Next(Summaries.Length)],
    //             City = city
    //         })
    //         .ToArray();
    // }
    //
    // [HttpGet("GetCurrent")]
    // public ActionResult<WeatherForecast> GetCurrent()
    // {
    //     var apiKey = "69883ee2fa7a4b6cec1f2856c0622922";
    //
    //     //Budapest lat & lon
    //     var lat = 47.497913;
    //     var lon = 19.040236;
    //
    //     try
    //     {
    //         var weatherData = _weatherDataProvider.GetCurrent(lat, lon);
    //         return Ok(_jsonProcessor.Process(weatherData));
    //     }
    //     catch (Exception e )
    //     {
    //         _logger.LogError(e, "Error getting weather data");
    //         return NotFound("Error getting weather data");
    //     }
    // }
    
    
    //Async Programming in ASP.NET Core
    [HttpGet("Get")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Long running process started");
        Thread.Sleep(10000);
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
    
    //GetAsync

    [HttpGet("GetAsync")]
    public async Task<IEnumerable<WeatherForecast>> GetAsync()
    {
        _logger.LogInformation("Long running process started");
        await Task.Delay(10000);
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
    
    [HttpGet("GetCurrent")]
    public async Task<ActionResult<WeatherForecast>> GetCurrent()
    {
        //Budapest lat & lon
        var lat = 47.497913;
        var lon = 19.040236;
    
        try
        {
            var weatherData = await _weatherDataProvider.GetCurrentAsync(lat, lon);
            return Ok(_jsonProcessor.Process(weatherData));
        }
        catch (Exception e )
        {
            _logger.LogError(e, "Error getting weather data");
            return NotFound("Error getting weather data");
        }
    }
    
}