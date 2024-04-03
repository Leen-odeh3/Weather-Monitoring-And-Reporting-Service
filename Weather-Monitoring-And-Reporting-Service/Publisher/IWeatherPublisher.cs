

using Weather_Monitoring_And_Reporting_Service.Subscriber;

namespace Weather_Monitoring_And_Reporting_Service.Publisher;

public interface IWeatherPublisher
{
    void Attach(IWeatherSubscriber observer);
    void Detach(IWeatherSubscriber observer);
    Task NotifyAsync();
}
