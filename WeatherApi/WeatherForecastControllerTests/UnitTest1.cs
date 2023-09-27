using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherApi;
using WeatherApi.Controllers;
using WeatherApi.Model;
using WeatherApi.Service;

namespace WeatherForecastControllerTests;

[TestFixture]
public class Tests
{
    private Mock<ILogger<WeatherForecastController>> _loggerMock;
    private Mock<IWeatherDataProvider> _weatherDataProviderMock;
    private Mock<IJsonProcessor> _jsonProcessorMock;
    private Mock<ICityRepository> _cityRepositoryMock;
    private WeatherForecastController _controller;
    
    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<WeatherForecastController>>();
        _weatherDataProviderMock = new Mock<IWeatherDataProvider>();
        _jsonProcessorMock = new Mock<IJsonProcessor>();
        _cityRepositoryMock = new Mock<ICityRepository>();
        _controller = new WeatherForecastController(_loggerMock.Object, _weatherDataProviderMock.Object, 
            _jsonProcessorMock.Object, _cityRepositoryMock.Object);
    }

    [Test]
    public async Task GetCurrentReturnsOkResultIfWeatherDataIsValid()
    {
        // Arrange
        var expectedForecast = new WeatherForecast();
        var weatherData = "{}";
        _weatherDataProviderMock.Setup(x => x.GetCurrentAsync(It.IsAny<double>(), It.IsAny<double>()))
            .ReturnsAsync(weatherData);
        
        _jsonProcessorMock.Setup(x => x.Process(weatherData)).Returns(expectedForecast);

        _cityRepositoryMock.Setup(x => x.GetByName("Budapest")).Returns(new City { Name = "Budapest" });
        // Act
        var result = await _controller.GetCurrent("Budapest");

        // Assert
        Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
        Assert.That(((OkObjectResult)result.Result).Value, Is.EqualTo(expectedForecast));
    }
}