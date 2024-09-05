using AutoFixture;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Public_API.Controllers;

using Services;

namespace Controller.test
{
    public class ControllerTest
    {
        private readonly IWeatherService _mockWeatherService = Mock.Of<IWeatherService>();
        private readonly IFixture _fixture = new Fixture();

        [Fact]
        public async Task GetWeather_ReturnsOkResult_WithWeatherData()
        {
            // Arrange
            var location = this._fixture.Create<Location>();
            var city = location.Name;
            var current = this._fixture.Create<Current>();

            Mock.Get(this._mockWeatherService)
                .Setup(service => service.GetWeatherAsync(city))
                .ReturnsAsync((location, current));

            var controller = new WeatherController(this._mockWeatherService);

            // Act
            var result = await controller.GetWeather(city);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task GetWeather_ReturnsServiceUnavailable_WhenHttpRequestExceptionIsThrown()
        {
            // Arrange
            var city = "London";

            Mock.Get(this._mockWeatherService)
                .Setup(service => service.GetWeatherAsync(city))
                .ThrowsAsync(new HttpRequestException("API is unreachable"));

            var controller = new WeatherController(this._mockWeatherService);

            // Act
            var result = await controller.GetWeather(city);

            // Assert
            var serviceUnavailableResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(503, serviceUnavailableResult.StatusCode);
        }

        [Fact]
        public async Task GetWeather_ReturnsBadRequest_WhenArgumentExceptionIsThrown()
        {
            // Arrange
            var city = "";

            Mock.Get(this._mockWeatherService)
                .Setup(service => service.GetWeatherAsync(city))
                .ThrowsAsync(new ArgumentException("City name is invalid"));

            var controller = new WeatherController(this._mockWeatherService);

            // Act
            var result = await controller.GetWeather(city);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetWeather_ReturnsInternalServerError_WhenUnexpectedExceptionIsThrown()
        {
            // Arrange
            var city = "London";

            Mock.Get(this._mockWeatherService)
                .Setup(service => service.GetWeatherAsync(city))
                .ThrowsAsync(new Exception("Unexpected error"));

            var controller = new WeatherController(this._mockWeatherService);

            // Act
            var result = await controller.GetWeather(city);

            // Assert
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, internalServerErrorResult.StatusCode);
        }
    }
}