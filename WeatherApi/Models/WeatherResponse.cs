namespace WeatherApi.Models
{
    public class WeatherResponse
    {
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
        public string Condition { get; set; } = string.Empty;
        public string Recommendation { get; set; } = string.Empty;
    }
}
