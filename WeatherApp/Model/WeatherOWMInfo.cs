using System.Text.Json.Serialization;

namespace WeatherApp.Model
{
    public class WeatherOWMInfo //данные из OpenWeatherMap
    {
        [JsonPropertyName("main")]
        public Dictionary<string, double> MainInfo { get; set; } = null!;
        [JsonPropertyName("clouds")]
        public Dictionary<string, double> Clouds { get; set; } = null!;
        [JsonPropertyName("rain")]
        public Dictionary<string, double> Rain { get; set; } = null!;
        [JsonPropertyName("snow")]
        public Dictionary<string, double> Snow { get; set; } = null!;
        [JsonPropertyName("wind")]
        public Dictionary<string, double> Wind { get; set; } = null!;

    }

}
