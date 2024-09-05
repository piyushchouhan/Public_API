using Microsoft.AspNetCore.Mvc;
using Services;

namespace Public_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            try
            {
                // Attempt to get the weather data
                var (location, current) = await _weatherService.GetWeatherAsync(city);

                // Return the weather data if successful
                return Ok(new { location, current });
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors (e.g., API not reachable)
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    new { Message = "Weather service is unavailable.", Details = ex.Message });
            }
            catch (ArgumentException ex)
            {
                // Handle invalid argument errors (e.g., null or empty city name)
                return BadRequest(new { Message = "Invalid city name.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle all other unexpected errors
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}
