using Weather_Monitoring_And_Reporting_Service.WeatherBot;
using Weather_Monitoring_And_Reporting_Service.Subscriber;
using Weather_Monitoring_And_Reporting_Service.Configuration;
using Weather_Monitoring_And_Reporting_Service.Strategies;
using static Weather_Monitoring_And_Reporting_Service.Weather;

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

    private List<IWeatherSubscriber> _subscribers = new List<IWeatherSubscriber>();
    private Weather newData;
    private ITextFormatStrategy textFormatStrategy;
    private BotConfiguration botConfig;

    public WeatherPublisher(string text, ITextFormatStrategy textFormat, BotConfiguration botConfig)
    {
        InitializeSubscribers(botConfig);

        _weatherData = textFormat.GetWeatherData(text);

        Notify();
    }

    public WeatherPublisher(Weather newData, ITextFormatStrategy textFormatStrategy, BotConfiguration botConfig)
    {
        this.newData = newData;
        this.textFormatStrategy = textFormatStrategy;
        this.botConfig = botConfig;
    }

    private void InitializeSubscribers(BotConfiguration botConfig)
    {
        Attach(botConfig.RainBot);
        Attach(botConfig.SnowBot);
        Attach(botConfig.SunBot);
    }

    public void Attach(IWeatherSubscriber observer)
    {
        _subscribers.Add(observer);
    }

    public void Detach(IWeatherSubscriber observer)
    {
        _subscribers.Remove(observer);
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
}

