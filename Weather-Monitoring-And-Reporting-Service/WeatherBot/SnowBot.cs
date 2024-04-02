

using Weather_Monitoring_And_Reporting_Service.Subscriber;
using static Weather_Monitoring_And_Reporting_Service.Weather;

namespace Weather_Monitoring_And_Reporting_Service.WeatherBot;

public class SnowBot : IWeatherSubscriber
{
    public bool Enabled { get; init; }
    public int TemperatureThreshold { get; init; }
    public string Message { get; init; }

    public void ProcessWeatherUpdate(Weather weatherData)
    {
        if (!Enabled) return;
        if (weatherData.Temperature < TemperatureThreshold)
        {
            Console.WriteLine("SnowBot Activated!");
            Console.WriteLine($"SnowBot: \"{Message}\"");
        }
    }
}