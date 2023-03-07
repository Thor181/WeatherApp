
namespace WeatherApp.Service
{
    internal class Config //настрйоки для приложения
    {
#warning Type your api key here
        internal static (double lat, double lon, string api) OpenWeatherMapApiConfig = (59.93863, 30.31413, "89b4319c02c111804aeed2c9dded286a");
        internal static (double lat, double lon, string api) TommorowIoApiConfig = (59.93863, 30.31413, "M5cHY0KsL51QLGbH11EknDPTQhxjRmcw");
    }
}
