using System.Net.Http.Json;
using WeatherApi.Models;


namespace WeatherApi.Repositories
{
    public interface IWeatherRepository
    {
        Task<WeatherApiResponse?> GetWeatherAsync(double lat, double lon);
    }
    public class WeatherRepository : IWeatherRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "5f404b57f624d1f425fdec0a43cb85a6"; // Replace this

        public WeatherRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherApiResponse?> GetWeatherAsync(double lat, double lon)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={_apiKey}&units=metric";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Optional: print the JSON for debugging
                // var json = await response.Content.ReadAsStringAsync();
                // Console.WriteLine(json);

                var data = await response.Content.ReadFromJsonAsync<WeatherApiResponse>();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching weather: {ex.Message}");
                return null;
            }
        }
    }
}
