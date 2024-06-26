﻿

using Weather_Monitoring_And_Reporting_Service.Subscriber;
using static Weather_Monitoring_And_Reporting_Service.Weather;

namespace Weather_Monitoring_And_Reporting_Service.WeatherBot;

public class SunBot : IWeatherSubscriber
{
    public bool Enabled { get; init; }
    public int TemperatureThreshold { get; init; }
    public string Message { get; init; }
    public bool Activated { get; private set; }

    public void ProcessWeatherUpdate(Weather weatherData)
    {
        if (!Enabled)
        {
            Console.WriteLine("SunBot is not enabled.");
            return;
        }

        if (weatherData.Temperature > TemperatureThreshold)
        {
            Activated = true;
            Console.WriteLine("SunBot Activated!");
            Console.WriteLine($"SunBot: \"{Message}\"");
        }
        else
        {
            Console.WriteLine($"SunBot not activated. Temperature {weatherData.Temperature} is not higher than the threshold {TemperatureThreshold}.");
        }
    }


}