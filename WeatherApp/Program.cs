using WeatherApp.Service;

namespace WeatherApp
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            Start();
        }
        
        private static void Start()
        {
            while (true) //тут бесконечный цикл, чтобы при работе приложения можно было несколько раз обновлять данные
            {
                Console.Clear();
                double lat = 0;
                double lon = 0;
                string apiKey = "";

                byte greetNumber = Renderer.Greetings();
                if (greetNumber == (byte)Renderer.Result.OpenWeatherMap)
                {
                    lat = Config.OpenWeatherMapApiConfig.lat;
                    lon = Config.OpenWeatherMapApiConfig.lon;
                    apiKey = Config.OpenWeatherMapApiConfig.api;
                    var data = new OpenWeatherMapApi(lat, lon, apiKey).GetData();
                    Renderer.Render(data);
                }
                else if (greetNumber == (byte)Renderer.Result.TommorowIo)
                {
                    lat = Config.TommorowIoApiConfig.lat;
                    lon = Config.TommorowIoApiConfig.lon;
                    apiKey = Config.TommorowIoApiConfig.api;
                    var data = new TomorrowIoApi(lat, lon, apiKey).GetData();
                    Renderer.Render(data);
                }
                else if (greetNumber == (byte)Renderer.Result.Exit)
                {
                    Environment.Exit(0);
                }
                
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
            }
        }
    }
    
}