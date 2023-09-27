using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherApi;
using WeatherApi.Controllers;
using WeatherApi.Service;

namespace WeatherForecastControllerTests;

[TestFixture]
public class Tests
{
    private Mock<ILogger<WeatherForecastController>> _loggerMock;
    private Mock<IWeatherDataProvider> _weatherDataProviderMock;
    private Mock<IJsonProcessor> _jsonProcessorMock;
    private WeatherForecastController _controller;
    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<WeatherForecastController>>();
        _weatherDataProviderMock = new Mock<IWeatherDataProvider>();
        _jsonProcessorMock = new Mock<IJsonProcessor>();
        _controller = new WeatherForecastController(_loggerMock.Object, _weatherDataProviderMock.Object, _jsonProcessorMock.Object);
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

        // Act
        var result = await _controller.GetCurrent();

        // Assert
        Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
        Assert.That(((OkObjectResult)result.Result).Value, Is.EqualTo(expectedForecast));
    }
}