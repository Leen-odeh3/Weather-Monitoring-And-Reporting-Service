using Weather_Monitoring_And_Reporting_Service.WeatherBot;
using Weather_Monitoring_And_Reporting_Service.Subscriber;
using Weather_Monitoring_And_Reporting_Service.Configuration;
using Weather_Monitoring_And_Reporting_Service.Strategies;

namespace Weather_Monitoring_And_Reporting_Service.Publisher;

public class WeatherPublisher : IWeatherPublisher
{

    private Weather _weatherData;
    public Weather WeatherData
    {
        set
        {
            _weatherData = value;
            Notify();
        }
    }

    private List<IWeatherBotSubscriber> _subscribers = new List<IWeatherBotSubscriber>();
    public WeatherPublisher(string text, ITextFormatStrategy textFormat, BotConfiguration botConfig)
    {
        InitializeSubscribers(botConfig);

        _weatherData = textFormat.GetWeatherData(text);

        Notify();
    }

    private void InitializeSubscribers(BotConfiguration botConfig)
    {
        Attach(botConfig.RainBot);
        Attach(botConfig.SnowBot);
        Attach(botConfig.SunBot);
    }

    public void Notify()
    {
        Console.WriteLine("Notifying observers .... ");
        Thread.Sleep(500);
        foreach (var subscriber in _subscribers)
        {
            subscriber.ProcessWeatherUpdate(_weatherData);
            Thread.Sleep(500);
        }
    }

    public void Attach(IWeatherSubscriber observer)
    {
        _subscribers.Add(observer);
    }

    public void Detach(IWeatherSubscriber observer)
    {
        _subscribers.Remove(observer);

    }
}
