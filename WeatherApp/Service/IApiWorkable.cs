
namespace WeatherApp.Service
{
    internal interface IApiWorkable //общий интерфейс для реализации классами подключения в АПИ
    {
        double Latitude { get; set; }
        double Longitude { get; set; }
        string ApiKey { get; set; }
        string AddressApi { get; set; }
        HttpClient HttpClient { get; set; }
    }
}
