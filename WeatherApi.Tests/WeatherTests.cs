using Xunit;
using WeatherApi.Models;
using WeatherApi.Services;
using WeatherApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApi.Tests
{
    public class WeatherTests
    {
        // A simple fake repository just to return data
        public class FakeWeatherRepository : IWeatherRepository
        {
            private readonly WeatherApiResponse _response;
            public FakeWeatherRepository(WeatherApiResponse response)
            {
                _response = response;
            }

            public Task<WeatherApiResponse?> GetWeatherAsync(double lat, double lon)
            {
                return Task.FromResult(_response);
            }
        }

        [Fact]
        public async Task SunnyDay_ShouldReturnHatRecommendation()
        {
            // Arrange
            var response = new WeatherApiResponse
            {
                Main = new MainData { Temp = 20 },
                Weather = new List<WeatherCondition> { new WeatherCondition { Main = "Sunny" } },
                Wind = new WindData { Speed = 5 }
            };

            var repo = new FakeWeatherRepository(response);
            var service = new WeatherService(repo);

            // Act
            var result = await service.GetWeatherAsync(0, 0);

            // Assert
            Assert.Equal("Don't forget to bring a hat", result.Recommendation);
            Assert.Equal("Sunny", result.Condition);
        }

        [Fact]
        public async Task HotDay_ShouldReturnSwimRecommendation()
        {
            var response = new WeatherApiResponse
            {
                Main = new MainData { Temp = 30 },
                Weather = new List<WeatherCondition> { new WeatherCondition { Main = "Sunny" } },
                Wind = new WindData { Speed = 5 }
            };

            var repo = new FakeWeatherRepository(response);
            var service = new WeatherService(repo);

            var result = await service.GetWeatherAsync(0, 0);

            Assert.Equal("It’s a great day for a swim", result.Recommendation);
        }

        [Fact]
        public async Task RainyColdDay_ShouldReturnCoatRecommendation()
        {
            var response = new WeatherApiResponse
            {
                Main = new MainData { Temp = 10 },
                Weather = new List<WeatherCondition> { new WeatherCondition { Main = "Rain" } },
                Wind = new WindData { Speed = 5 }
            };

            var repo = new FakeWeatherRepository(response);
            var service = new WeatherService(repo);

            var result = await service.GetWeatherAsync(0, 0);

            Assert.Equal("Don't forget to bring a coat", result.Recommendation);
        }
    }
}
