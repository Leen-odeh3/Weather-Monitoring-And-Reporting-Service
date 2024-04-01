using Weather_Monitoring_And_Reporting_Service.WeatherBot;
using Weather_Monitoring_And_Reporting_Service.Subscriber;
using Weather_Monitoring_And_Reporting_Service.Configuration;
using Weather_Monitoring_And_Reporting_Service.Strategies;

namespace Weather_Monitoring_And_Reporting_Service.Publisher;

public class WeatherPublisher : IWeatherPublisher
{

    private Weather _weather;
    public Weather Weather
    {
        set
        {
            _weather = value;
            Notify();
        }
    }

    private List<IWeatherSubscriber> _subscribers = new List<IWeatherSubscriber>();
    public WeatherPublisher(string text, ITextFormatStrategy textFormat, BotConfiguration botConfig)
    {
        InitializeSubscribers(botConfig);

        _weather = textFormat.GetWeatherData(text);

        Notify();
    }

    private void InitializeSubscribers(BotConfiguration botConfig)
    {
        Attach((IWeatherSubscriber)botConfig.RainBot);
        Attach((IWeatherSubscriber)botConfig.SnowBot);
        Attach((IWeatherSubscriber)botConfig.SunBot);
    }

    public void Notify()
    {
        Console.WriteLine("Notifying observers .... ");
        Thread.Sleep(500);
        foreach (var subscriber in _subscribers)
        {
            subscriber.ProcessWeatherUpdate(_weather);
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
