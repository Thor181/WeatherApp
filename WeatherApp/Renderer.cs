using WeatherApp.Model;

namespace WeatherApp
{
    internal class Renderer //класс для отображения сведений в консоли
    {
        internal enum Result : byte
        {
            Empty = 0,
            OpenWeatherMap = 1,
            TommorowIo = 2,
            Exit = 3
        }
        internal static void Render<T>(T weatherInfo)
        {
            ReadyWeatherInfo readyData = new();

            if (weatherInfo is WeatherOWMInfo owmData)
            {
                readyData = new ReadyWeatherInfo(owmData);
            }
            else if (weatherInfo is WeatherTIInfo tData)
            {
                readyData = new ReadyWeatherInfo(tData);
            }
            
            if (readyData.CheckStatus() == false)
            {
                return;
            }
            Console.WriteLine();

            Console.Write($"Температура:  ");
            if (readyData.TempF > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            Console.Write($"[{readyData.TempF:#.##}°F] ");
            if (readyData.TempC > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            Console.Write($"[{readyData.TempC:#.##}°C] ");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine($"Облачность: [{readyData.CloudsPercent}%]");
            Console.WriteLine($"Влажность: [{readyData.Humidity}%]");
            Console.Write($"Осадки: ");
            if (readyData.RainPercent != null || readyData.SnowPercent != null)
            {  
                if (readyData.SnowPercent == 0 && readyData.RainPercent == 0)
                {
                    Console.Write("[отсутствуют]"); 
                }
                if (readyData.RainPercent != 0 && readyData.RainPercent != null)
                { 
                    Console.Write($"[дождь - {readyData.RainPercent} мм]");
                }
                if (readyData.SnowPercent != 0 && readyData.SnowPercent != null)
                {
                    Console.Write($"[снег - {readyData.SnowPercent} мм]");
                }
            }
            else
            {
                Console.Write("[отсутствуют]");
            }
            Console.WriteLine();
            Console.WriteLine($"Ветер: [{readyData.WindSpeed} м/с - {readyData.WindDirection}]");
            Console.WriteLine();

        }
        internal static void ShowErrorConnection()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("В данный момент сервис недоступен!");
            Console.ResetColor();
        }
        internal static byte Greetings() //сообщение с выбором действий при запуске
        {
            Console.WriteLine("Выберите действие...\n1) Обновить\n2) Выйти");
            byte choose = (byte)Result.Empty;
            byte.TryParse(Console.ReadLine(), out choose);
            if (choose != 1 && choose != 2)
            {
                NonValid();
                Greetings();
            }

            if (choose == 1)
            {
                Console.WriteLine("Выберите сервис...\n1) OpenWeatherMap\n2) Tommorow");
                byte innerChoose = (byte)Result.Empty;
                byte.TryParse(Console.ReadLine(), out innerChoose);
                if (innerChoose != 1 && innerChoose != 2)
                {
                    NonValid();
                    Greetings();
                }
                if (innerChoose == (byte)Result.OpenWeatherMap || innerChoose == (byte)Result.TommorowIo)
                {
                    return innerChoose;
                }
            }
            return (byte)Result.Exit;
        }
        private static void NonValid() //отображение ошибки
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.WriteLine("Введены неверные символы!");
            Console.ResetColor();
        }
    }
}
