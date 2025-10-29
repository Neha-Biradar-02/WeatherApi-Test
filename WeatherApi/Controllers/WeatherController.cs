using Microsoft.AspNetCore.Mvc;
using WeatherApi.Services;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(double lat, double lon)
        {
            try
            {
                var result = await _weatherService.GetWeatherAsync(lat, lon);
                if (result == null) return NotFound(new { message = "Weather data not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Simulated upstream service failure"))
                    return StatusCode(503, new { message = "Service Unavailable" });

                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
