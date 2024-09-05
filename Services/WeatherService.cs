using Data;
using Data.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;
        private readonly string _apiKey;
        private readonly IConfiguration configuration;

        public WeatherService(HttpClient httpClient, ApplicationDbContext context, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _context = context;
            _apiKey = configuration["WeatherApi:Key"];
        }

        public async Task<(Location, Current)> GetWeatherAsync(string city)
        {
            var response = await _httpClient.GetAsync($"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}&aqi=no");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            // Deserialize each part of the response
            var location = JsonConvert.DeserializeObject<Location>(JObject.Parse(content)["location"].ToString());
            var current = JsonConvert.DeserializeObject<Current>(JObject.Parse(content)["current"].ToString());

            // Save the data
            await SaveWeatherDataAsync(location, current);

            // Return the saved data
            return (location, current);
        }

        public async Task SaveWeatherDataAsync(Location location, Current current)
        {
            // Save the Condition first to get its Id
            var condition = current.Condition;
            _context.Conditions.Add(condition);
            await _context.SaveChangesAsync();

            // Associate the saved condition with the current weather
            current.Condition = condition;
            _context.Currents.Add(current);
            await _context.SaveChangesAsync();

            // Save the Location
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
        }
    }
}
