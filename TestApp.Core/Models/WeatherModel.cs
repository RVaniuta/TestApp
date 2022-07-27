using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestApp.Core.Models
{
    public class WeatherModel
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("weatherCondition")]
        public WeatherCondition WeatherCondition { get; set; }

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }
    }

    public class WeatherCondition
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("pressure")]
        public double Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }
    }

    public class Wind
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }

        [JsonPropertyName("direction")]
        public string Direction { get; set; }
    }
}
