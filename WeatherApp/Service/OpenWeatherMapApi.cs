using WeatherApp.Model;
using System.Text.Json;

namespace WeatherApp.Service
{
    public class OpenWeatherMapApi : IApiWorkable //класс подключения к апи OpenWeatherMap
    {
        public double Latitude { get; set; } //широта
        public double Longitude { get; set; } //долгота
        public string ApiKey { get; set; }
        public HttpClient HttpClient { get; set; }
        public string AddressApi { get; set; }
        public OpenWeatherMapApi(double latitude, double longitude, string apiKey)
        {
            Latitude = latitude;
            Longitude = longitude;
            ApiKey = apiKey;
            AddressApi = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}";

            HttpClient = new HttpClient();
        }
        
        public WeatherOWMInfo GetData() //получить данные из OpenWeatherMap
        {
            HttpResponseMessage response;
            try
            {
                response = HttpClient.GetAsync(AddressApi).Result;
            }
            catch (Exception)
            {
                Renderer.ShowErrorConnection();
                return null!;
            }
            
            var responseBody = response.Content.ReadAsStringAsync().Result;
            var model = JsonSerializer.Deserialize<WeatherOWMInfo>(responseBody) ?? new WeatherOWMInfo();
            return model;
        }
    }
}
