using System.Text.Json.Serialization;

namespace WeatherApp.Model
{
    public class WeatherTIInfo //данные из TomorrowIo
    {
        [JsonPropertyName("data")]
        public Timeline MainData { get; set; } = null!;
    }
    public class Timeline
    {
        [JsonPropertyName("timelines")]
        public List<Intervals> Timelines { get; set; } = null!;
    }
    public class Intervals
    {
        [JsonPropertyName("intervals")]
        public List<Values> Values { get; set; } = null!;
    }
    public class Values
    {
        [JsonPropertyName("values")]
        public WeatherValues WeatherValues { get; set; } = null!;
    }
    public class WeatherValues
    {
        [JsonPropertyName("cloudCover")]
        public double CloudBase { get; set; }
        [JsonPropertyName("precipitationIntensity")]
        public double PrecipitationIntensity { get; set; }
        [JsonPropertyName("precipitationType")]
        public double PrecipitationType { get; set; }
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }
        [JsonPropertyName("windDirection")]
        public double WindDirection { get; set; }
        [JsonPropertyName("windSpeed")]
        public double WindSpeed { get; set; }
        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }
    }

}
