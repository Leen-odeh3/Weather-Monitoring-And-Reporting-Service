

using Weather_Monitoring_And_Reporting_Service.Subscriber;

namespace Weather_Monitoring_And_Reporting_Service.Publisher;

public interface IWeatherPublisher
{
    public void Attach(IWeatherSubscriber observer);

    public void Detach(IWeatherSubscriber observer);

    public void Notify();
}
