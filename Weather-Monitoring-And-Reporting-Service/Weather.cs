
using Weather_Monitoring_And_Reporting_Service.Publisher;
using Weather_Monitoring_And_Reporting_Service.Subscriber;

namespace Weather_Monitoring_And_Reporting_Service;

internal class Weather : IWeatherPublisher
{
    public int Humidity { get; init; }
    public int Temperature { get; init; }

    private List<IWeatherSubscriber> _subscribers = new List<IWeatherSubscriber>();

    public void Notify()
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.ProcessWeatherUpdate(this);
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
