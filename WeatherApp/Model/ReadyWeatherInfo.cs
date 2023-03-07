
namespace WeatherApp.Model
{
    public class ReadyWeatherInfo //класс для преобразования данных в готовый вид
    {
        internal double? TempF { get; set; } //температура по фаренгейту
        internal double? TempC { get; set; } //темп. по цельсью

        internal double? CloudsPercent { get; set; } //процент облачности

        internal double? RainPercent { get; set; } //процент дождя
        internal double? SnowPercent { get; set; } //процент снега

        internal double? Humidity { get; set; } //влажность 

        internal double? WindSpeed { get; set; } //скорсоть ветра
        internal string? WindDirection { get; set; } //направление ветра
        public ReadyWeatherInfo() //пустой конструктор нужен для работы
        {

        }
        public ReadyWeatherInfo(WeatherOWMInfo weatherInfo) //конструктор для преобразования данных из OpenWeatherMap
        {
            if (weatherInfo == null )
            {
                return;
            }
            double? tempK = weatherInfo.MainInfo?.Where(x => x.Key == "temp").Select(x => x.Value).FirstOrDefault(); 
            TempF = tempK != null ? (tempK - 273.15) * 9 / 5 + 32 : null;
            TempC = tempK != null ? tempK - 273.15 : null;
            try
            {
                CloudsPercent = weatherInfo.Clouds["all"];
                RainPercent = weatherInfo.Rain?["1h"];
                SnowPercent = weatherInfo.Snow?["1h"];
                Humidity = weatherInfo?.MainInfo?["humidity"];
                WindSpeed = weatherInfo?.Wind["speed"];
                WindDirection = GetWindDirection(weatherInfo?.Wind["deg"]);
            }
            catch (Exception)
            {
                Renderer.ShowErrorConnection();
                Environment.Exit(0);
            }  
        }
        public ReadyWeatherInfo(WeatherTIInfo weatherInfo) //конструктор для преобразования данных из tomorrowIo
        {
            if (weatherInfo == null)
            {
                return;
            }
            try
            {
                WeatherValues weatherValues = weatherInfo.MainData.Timelines[0].Values[0].WeatherValues;
                TempC = weatherValues.Temperature;
                TempF = weatherValues.Temperature * 9 / 5 + 32;
                CloudsPercent = weatherValues.CloudBase;
                RainPercent = weatherValues.PrecipitationIntensity;
                SnowPercent = weatherValues.PrecipitationIntensity;
                Humidity = weatherValues.Humidity;
                WindSpeed = weatherValues.WindSpeed;
                WindDirection = GetWindDirection(weatherValues.WindDirection);
            }
            catch (Exception)
            {
                Renderer.ShowErrorConnection();
                Environment.Exit(0);
            }  
        }
        

        public string GetWindDirection(double? deg) //на вход подается направление ветра, выдается строка с направление
        {
            if (deg == null) return null;
            string north = "Северный";
            string northEast = "Северо-Восточный";
            string east = "Восточный";
            string southEast = "Юго-Восточный";
            string south = "Южный";
            string southWest = "Юго-Западный";
            string west = "Западный";
            string northWest = "Северо-Западный";

            string direct = deg switch
            {
                >= 0 and <= 22.5 => north,
                > 22.5 and <= 67.5 => northEast,
                > 67.5 and <= 112.5 => east,
                > 112.5 and <= 157.5 => southEast,
                > 157.5 and <= 202.5 => south,
                > 202.5 and <= 247.5 => southWest,
                > 247.5 and <= 292.5 => west,
                > 292.5 and <= 337.5 => northWest,
                > 337.5 and <= 360 => north,
                _ => throw new ArgumentOutOfRangeException(nameof(deg))
            };
            return direct;
        }
        ///<summary>Проверить статус текущего объекта</summary>
        /// <returns>true - если данные имеются; false - если все данные null</returns>
        internal bool CheckStatus()
        {
            return this.GetType().GetProperties(System.Reflection.BindingFlags.Instance | 
                                                System.Reflection.BindingFlags.NonPublic | 
                                                System.Reflection.BindingFlags.Public).Any(x => x != null); //проверяет что хотя-бы одно поле в классе не null
        }
    }
}
