namespace WeatherApi.Models
{
    public class WeatherApiResponse
    {
        public List<WeatherCondition>? Weather { get; set; }
        public MainData? Main { get; set; }
        public WindData? Wind { get; set; }
        public string? Name { get; set; }
    }

    public class WeatherCondition
    {
        public string? Main { get; set; }
        public string? Description { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
        public double Feels_Like { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }

    public class WindData
    {
        public double Speed { get; set; }
    }
}
