using Data.Models;

namespace Services
{
    public interface IWeatherService
    {
        /// <summary>
        /// Retrieves weather information for the specified city.
        /// </summary>
        /// <param name="city">The name of the city to retrieve weather information for.</param>
        /// <returns>A tuple containing the location and current weather conditions.</returns>
        Task<(Location location, Current current)> GetWeatherAsync(string city);

        /// <summary>
        /// Saves the weather data to the database.
        /// </summary>
        /// <param name="location">The location information to save.</param>
        /// <param name="current">The current weather conditions to save.</param>
        Task SaveWeatherDataAsync(Location location, Current current);
    }
}
