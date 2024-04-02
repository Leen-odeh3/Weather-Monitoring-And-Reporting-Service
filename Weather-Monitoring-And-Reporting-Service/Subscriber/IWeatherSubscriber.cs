

using static Weather_Monitoring_And_Reporting_Service.Weather;

namespace Weather_Monitoring_And_Reporting_Service.Subscriber;

public interface IWeatherSubscriber
{
    public void ProcessWeatherUpdate(Weather weatherPublisher);

}
