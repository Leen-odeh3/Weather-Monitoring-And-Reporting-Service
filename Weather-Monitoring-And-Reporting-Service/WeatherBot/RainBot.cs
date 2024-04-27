
using Weather_Monitoring_And_Reporting_Service.Subscriber;

namespace Weather_Monitoring_And_Reporting_Service.WeatherBot
{
    public class RainBot : IWeatherSubscriber
    {
        public bool Enabled { get; init; }
        public int HumidityThreshold { get; init; }
        public string Message { get; init; }
        public bool Activated { get; private set; }

        public void ProcessWeatherUpdate(Weather weatherData)
        {
            if (!Enabled) return;
            if (weatherData.Humidity > HumidityThreshold)
            {
                Console.WriteLine("RainBot Activated!");
                Console.WriteLine($"RainBot: \"{Message}\"");
                Activated = true;
            }
        }
    }
}
