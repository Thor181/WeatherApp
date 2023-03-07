using System.Globalization;
using System.Text.Json;
using WeatherApp.Model;

namespace WeatherApp.Service
{
    public class TomorrowIoApi : IApiWorkable //класс данных из TomorrowIoApi
    {
        public double Latitude { get; set; } //широта
        public double Longitude { get; set; } //долгота
        public string ApiKey { get; set; } // ключ апи
        public string AddressApi { get; set; } //адрес апи
        public HttpClient HttpClient { get; set; }

        public TomorrowIoApi(double latitude, double longitude, string apiKey)
        {
            Latitude = latitude;
            Longitude = longitude;
            ApiKey = apiKey;
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            string lat = latitude.ToString(nfi);
            string lon = longitude.ToString(nfi);
            AddressApi = $"https://api.tomorrow.io/v4/timelines?location={lat},{lon}&fields=temperature,windSpeed,precipitationIntensity,precipitationType,windDirection,humidity,cloudCover&timesteps=current&units=metric&apikey={apiKey}";
            HttpClient = new HttpClient();
        }

        public WeatherTIInfo GetData()
        {
            HttpResponseMessage response;
            try
            {
                response = HttpClient.GetAsync(AddressApi).Result; //получам данные из апи
            }
            catch (Exception)
            {
                Renderer.ShowErrorConnection();
                return null!;
            }
            var responseBody = response.Content.ReadAsStringAsync().Result;
            var model = JsonSerializer.Deserialize<WeatherTIInfo>(responseBody) ?? new WeatherTIInfo();
            return model;
        }
    }
}
