using WeatherApi.Models;
using WeatherApi.Repositories;

namespace WeatherApi.Services
{
    public class WeatherService
    {
        private readonly IWeatherRepository _repository;
        private static int _requestCount = 0;

        public WeatherService(IWeatherRepository repository)
        {
            _repository = repository;
        }

        public async Task<WeatherResponse?> GetWeatherAsync(double lat, double lon)
        {
            // Simulate every 5th request as a failure
            _requestCount++;
            if (_requestCount % 5 == 0)
            {
                throw new Exception("Simulated upstream service failure");
            }

            var data = await _repository.GetWeatherAsync(lat, lon);
            if (data == null) return null;

            var condition = data.Weather?.FirstOrDefault()?.Main ?? "Unknown";

            var weather = new WeatherResponse
            {
                Temperature = data.Main?.Temp ?? 0,
                WindSpeed = data.Wind?.Speed ?? 0,
                Condition = condition,
                Recommendation = GetRecommendation(data.Main?.Temp ?? 0, condition)
            };

            return weather;
        }

        private string GetRecommendation(double temperature, string condition)
        {
            if (temperature > 25)
                return "It’s a great day for a swim";

            if (temperature < 15 &&
                (condition.Equals("Rain", StringComparison.OrdinalIgnoreCase) ||
                 condition.Equals("Snow", StringComparison.OrdinalIgnoreCase)))
                return "Don't forget to bring a coat";

            if (condition.Equals("Rain", StringComparison.OrdinalIgnoreCase))
                return "Don’t forget the umbrella";

            if (condition.Equals("Sunny", StringComparison.OrdinalIgnoreCase))
                return "Don't forget to bring a hat";

            return "Dress comfortably for the weather";
        }

    }
}
